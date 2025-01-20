namespace SGII_Back.Dominio.Interfaces.Services;

public interface IAuthService
{
    bool Autenticate(int userId, string password);
    (string Hash, string Salt) CreateHash(string password);

}
