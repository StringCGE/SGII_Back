namespace SGII_Back.Dominio.Interfaces.Services;

public interface IAuthService
{
    Task<bool> Autenticate(int? userId, string password);
    Task<int> Intentos(int? userId);
    (string Hash, string Salt) CreateHash(string password);

}
