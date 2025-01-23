using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Services.AuthApp;
using System.IdentityModel.Tokens.Jwt;


using SGII_Back.Application.DTO.AuthDTO;
using SGII_Back.Dominio.Entities;
using SGII_Back.Dominio.Interfaces.Repositories;
using SGII_Back.Dominio.Interfaces.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SGII_Back.Application.Services.AuthApp;
using SGII_Back.Dominio.Interfaces.Services;
using SGII_Back.Dominio.Interfaces.Services;
using SGII_Back.Dominio.Interfaces.Repositories;
using Dominio;
using Dominio.DB;


namespace Application.Services.AuthApp;

public class AuthAppService : IAuthAppService
{

    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    //private readonly IUnitOfWork _unitOfWork;


    public AuthAppService(IConfiguration configuration, IAuthService authService, IUserService userService/*, IUnitOfWork unitOfWork*/)
    {
        _configuration = configuration;
        _authService = authService;
        _userService = userService;
        //_unitOfWork = unitOfWork;
    }

    public async Task<AuthDTO> Login(AuthRequest login)
    {
        var validUserResult = await IsValidUser(login.User);
         AuthDTO au = new AuthDTO();
        if (validUserResult.IsValid && validUserResult.User != null) {
            if (await _authService.Autenticate(validUserResult.User.id, login.Password)) {
                return new AuthDTO(GenerateJwtToken(validUserResult.User), DateTimeOffset.Now);
            }
            au.intentos = await new DbUser().ObtenerIntentosPorIdAsync(validUserResult.User.id ?? 0);
            au.usuarioBloqueado = false;
        }
        if (!validUserResult.IsValid && validUserResult.User != null)
        {
            au.intentos = await new DbUser().ObtenerIntentosPorIdAsync(validUserResult.User.id ?? 0);
            au.usuarioBloqueado = true;
        }
        else{
            au.intentos = -1;
        }
        return au;
    }

    private async Task<(bool IsValid, ClsUser? User)> IsValidUser(string email)
    {
        ClsUser? user = await _userService.GetByEmail(email);
        if(user == null) return (IsValid: false, User: null);
        if (user.intentos > 5) return (IsValid: false, User: user);//Si el usuario es no valido peor existe instancia de usuario, usuario bloqueado
        return (IsValid: user != null, User: user);
    }

    private string GenerateJwtToken(ClsUser user)
    {
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.id.ToString()),
                new Claim("persona_id", user.persona.id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim("urlFoto", user.urlFoto),
                new Claim(ClaimTypes.Role, user.role),
                new Claim("estado", user.estado.ToString()),
                new Claim("idPersReg", user.idPersReg.ToString()),
                new Claim("dtReg", user.dtReg.ToString()),
                //new Claim(JwtRegisteredClaimNames.Sub, user.nombre
                //new Claim(ClaimTypes.Role, "SuperAdmin")
                //new Claim("Rol", user.rol),
                /* Claim("usuario", user.Identification),
                new Claim("nombre", user.nombre),
                new Claim("apellido", user.apellido),
                new Claim("cedula", user.cedula),*/
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            Issuer = "emisor",//_configuration["Jwt:Issuer"],
            Audience = "audiencia",//_configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes("54vD8gL9MfQ7sT2yX1pW5K6zC3nH0bJ8dA4qY7vX9tR2L6mP5wZ3N1pQ8kT7sF6y")),//_configuration["Jwt:Key"]!
                SecurityAlgorithms.HmacSha512Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<ResetPasswordDTO> SendCodeToResetPassword(SendCodeToResetPasswordRequest login)
    {
        

        return new ResetPasswordDTO(false, "No se encontró el usuario");
    }

    public async Task<ResetPasswordDTO> ResetPassword(ResetPasswordRequest login)
    {
        (bool IsValid, ClsUser? User) _validUser = await IsValidUser(login.Identification);
        if (_validUser.IsValid)
        {
            if (_validUser.User != null)
            {
                if (CodeIsValid(_validUser.User.id, login.Code))
                {

                    ClsUser? _user = _userService.GetById(_validUser.User.id);
                    if (_user != null)
                    {
                        _user.password = login.Password;
                        _userService.UpdatePassword(_user);
                        //_unitOfWork.SaveChanges();
                        return new ResetPasswordDTO(true, "Clave actualizada");
                    }
                }
                else
                {
                    return new ResetPasswordDTO(false, "El código es inválido");
                }
            }

        }

        return new ResetPasswordDTO(false, "No se encontró el usuario");
    }


    private string GenerateUniqueCode(int userId, int length = 6)
    {
        const string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        byte[] randomBytes = new byte[length];

        using (RandomNumberGenerator cryptoProvider = RandomNumberGenerator.Create())
        {
            cryptoProvider.GetBytes(randomBytes);
        }

        char[] characters = new char[length];
        int allowedCharactersLength = allowedCharacters.Length;

        for (int i = 0; i < length; i++)
        {
            characters[i] = allowedCharacters[randomBytes[i] % allowedCharactersLength];
        }

        string code = new(characters);

        if (CodeIsUnique(code))
        {
            SaveCodeInDatabase(userId, code);
            return code;
        }

        return GenerateUniqueCode(userId, length);
    }

    private bool CodeIsUnique(string code)
    {
        /*var time = DateTime.Now.AddMinutes(-5);
        return _userService.GetAll(a => a.TempCode == code && a.TempCodeCreateAt >= time).FirstOrDefault() == null;*/
        return false;
    }

    private bool CodeIsValid(int? userId, string code)
    {
        /*var time = DateTime.Now.AddMinutes(-5);
        return _userService.GetAll(a => a.Id == userId && a.TempCode == code && a.TempCodeCreateAt >= time).FirstOrDefault() != null;*/
        return false;
    }

    private void SaveCodeInDatabase(int userId, string code)
    {
        ClsUser? _user = null;// _userService.GetById(userId);
        if (_user != null)
        {
            _user.tempCode = code;
            _user.tempCodeCreateAt = DateTime.Now;
            /*_userService.Update(_user);
            _unitOfWork.SaveChanges();*/
        }
        else
        {
            throw new Exception("No se encontró el usuario");
        }
    }

}
