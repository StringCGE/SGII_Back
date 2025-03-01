﻿using Dominio;
using Dominio.DB;
using SGII_Back.Dominio.Entities;
using SGII_Back.Dominio.Interfaces.Repositories;
using SGII_Back.Dominio.Interfaces.Services;
using SGII_Back.Dominio.Shared.Interfaces;

using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace SGII_Back.Dominio.Services;

public class UserService : IUserService
{
    //private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    //private readonly IClaimsService _claimsService;
    //private readonly IUnitOfWork _unitOfWork;

    public UserService(
        //IUserRepository userRepository,
        IAuthService authService/*,
        IClaimsService claimsService,
        IUnitOfWork unitOfWork*/)
    {
        //_userRepository = userRepository;
        _authService = authService;
        //_claimsService = claimsService;
        //_unitOfWork = unitOfWork;
    }


    public ClsUser? GetById(int? userId)
    => null;

    public IEnumerable<ClsUser> GetAll(Expression<Func<ClsUser, bool>>? filter = null)
        => null;

    public async Task<bool> UserNameExist(string name)
    {
        ClsUser? user = await new DbUser().ObtenerPorEmailAsync(name);
        //if (_userRepository.GetAll(a => a.Identification == name).FirstOrDefault() is not null) return true;
        bool value = user != null;
        return value;

        /*IEnumerable<User> users = _userRepository.GetAll(a => a.Name == name);
        return users.Count() > 0;*/
    }

    public void Create(ClsUser user)
    {
        /*user.Identification = user.Identification.ToLower();
        if (_userRepository.GetAll(a => a.Identification == user.Identification).FirstOrDefault() is not null)
            throw new Exception("Ya existe un usuario con esa identificación");
        var result = _authService.CreateHash(user.Password);
        user.Password = result.Hash;
        user.Salt = result.Salt;

        if (user is IAudit)
        {
            user.CreatedAt = DateTime.Now;
            user.CreatedBy = _claimsService.UserId;
        }

        _userRepository.Insert(user);

        _unitOfWork.SaveChanges();*/
    }

    public void Update(ClsUser user)
    {/*
        if (user is IAudit && _claimsService.Autenticated)
        {
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = _claimsService.UserId;
        }
        _userRepository.Update(user);
        _unitOfWork.SaveChanges();*/
    }

    public void UpdatePassword(ClsUser user)
    {
        var result = _authService.CreateHash(user.password);
        user.password = result.Hash;
        user.salt = result.Salt;
        user.tempCode = null;
        user.tempCodeCreateAt = null;

        /*if (user is IAudit && _claimsService.Autenticated)
        {
            user.CreatedAt = DateTime.Now;
            user.CreatedBy = _claimsService.UserId;
        }*/

        /*_userRepository.Update(user);*/
    }








    public async Task<bool> RecoverPasswordAsync(string username, string email)
    {
        /*var user = _userRepository.GetAll(u => u.Identification == username && u.Email == email).FirstOrDefault();
        if (user == null)
            return false;

        // Generar una nueva contraseña temporal
        var newPassword = GenerateRandomPassword();
        var result = _authService.CreateHash(newPassword);
        user.Password = result.Hash;
        user.Salt = result.Salt;

        // Actualizar usuario con la nueva contraseña
        _userRepository.Update(user);
        _unitOfWork.SaveChanges();

        // Enviar la nueva contraseña por correo electrónico
        var subject = "Recuperación de Contraseña";
        var body = $"Su nueva contraseña es: {newPassword}";
        await _smtpConfigurationService.SendEmailAsync(email, subject, body);*/

        return true;
    }

    public async Task<ClsUser?> GetByEmail(string email)
    {
        DbUser db_user = new DbUser();
        ClsUser? user = await db_user.ObtenerPorEmailAsync(email);
        return user;
    }

    /*public async Task<bool> SendConfirmationEmailAsync(string email)
    {
        var user = _userRepository.GetAll(u => u.Email == email).FirstOrDefault();
        if (user == null)
            return false;

        var confirmationCode = GenerateConfirmationCode();
        await _confirmationCodeRepository.SaveCodeAsync(user.Id, confirmationCode);

        var subject = "Confirmación de Correo Electrónico";
        var body = $"Su código de confirmación es: {confirmationCode}";
        await _emailSender.SendEmailAsync(email, subject, body);

        return true;
    }

    public async Task<bool> ConfirmEmailAsync(string email, string code)
    {
        var isValidCode = await _confirmationCodeRepository.ValidateCodeAsync(email, code);
        if (!isValidCode)
            return false;

        var user = _userRepository.GetAll(u => u.Email == email).FirstOrDefault();
        if (user == null)
            return false;

        // Confirmar el correo electrónico del usuario
        user.IsEmailConfirmed = true;
        _userRepository.Update(user);
        await _confirmationCodeRepository.RemoveCodeAsync(email, code);

        _unitOfWork.SaveChanges();
        return true;
    }*/

}

