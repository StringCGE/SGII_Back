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

    public AuthDTO Login(AuthRequest login)
    {
        (bool IsValid, User? User) _validUser = IsValidUser(login.User);

        if (_validUser.IsValid && _validUser.User != null)
        {
            if (_authService.Autenticate(_validUser.User.Id, login.Password))
            {
                return new AuthDTO(GenerateJwtToken(_validUser.User), DateTimeOffset.Now);
            }
        }

        return new AuthDTO();
    }

    private (bool IsValid, User? User) IsValidUser(string email)
    {
        User? _user = _userService.GetByEmail(email);
        return (IsValid: _user != null, User: _user);
    }

    private string GenerateJwtToken(User user)
    {
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                //new Claim(JwtRegisteredClaimNames.Sub, user.nombre
                //new Claim(ClaimTypes.Role, "SuperAdmin")
                //new Claim("Rol", user.rol),
                new Claim(ClaimTypes.Role, user.role),
                /* Claim("usuario", user.Identification),
                new Claim("nombre", user.nombre),
                new Claim("apellido", user.apellido),
                new Claim("cedula", user.cedula),
                new Claim("nacimiento", user.nacimiento.ToString()),*/
                new Claim("urlFoto", user.urlFoto),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!)),
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

    public ResetPasswordDTO ResetPassword(ResetPasswordRequest login)
    {
        (bool IsValid, User? User) _validUser = IsValidUser(login.Identification);
        if (_validUser.IsValid)
        {
            if (_validUser.User != null)
            {
                if (CodeIsValid(_validUser.User.Id, login.Code))
                {

                    User? _user = _userService.GetById(_validUser.User.Id);
                    if (_user != null)
                    {
                        _user.Password = login.Password;
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

    private bool CodeIsValid(int userId, string code)
    {
        /*var time = DateTime.Now.AddMinutes(-5);
        return _userService.GetAll(a => a.Id == userId && a.TempCode == code && a.TempCodeCreateAt >= time).FirstOrDefault() != null;*/
        return false;
    }

    private void SaveCodeInDatabase(int userId, string code)
    {
        User? _user = null;// _userService.GetById(userId);
        if (_user != null)
        {
            _user.TempCode = code;
            _user.TempCodeCreateAt = DateTime.Now;
            /*_userService.Update(_user);
            _unitOfWork.SaveChanges();*/
        }
        else
        {
            throw new Exception("No se encontró el usuario");
        }
    }

}
