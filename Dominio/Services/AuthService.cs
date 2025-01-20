using System.Security.Cryptography;
using System.Text;
using SGII_Back.Dominio.Entities;
using SGII_Back.Dominio.Interfaces.Repositories;
using SGII_Back.Dominio.Interfaces.Services;

namespace SGII_Back.Dominio.Services;

public class AuthService : IAuthService
{
    //private readonly IUserRepository _userRepository;

    public AuthService(/*IUserRepository usuarioRepository*/)
    {
        //_userRepository = usuarioRepository;
    }

    public bool Autenticate(int userId, string password)
    {
        /*User? _user = _userRepository.GetById(userId);
        if (_user == null)
        {

        }*/
        return VerifyPassword(password, "",""/*_user.Password, _user.Salt*/);
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);

        using HMACSHA256 hmac = new(saltBytes);
        byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);
        byte[] computedHashBytes = hmac.ComputeHash(enteredPasswordBytes);

        string computedHash = Convert.ToBase64String(computedHashBytes);

        return storedHash.Equals(computedHash);
    }

    private string GenerarSalt()
    {
        byte[] randomBytes = new byte[32];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }

    public (string Hash, string Salt) CreateHash(string password)
    {
        string salt = GenerarSalt();
        using HMACSHA256 hmac = new(Convert.FromBase64String(salt));
        byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return (Hash: Convert.ToBase64String(hashBytes), Salt: salt);
    }

    public static (string Hash, string Salt) CreateHashStatic(string password)
    {
        string salt = GenerarSaltStatic();
        using HMACSHA256 hmac = new(Convert.FromBase64String(salt));
        byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return (Hash: Convert.ToBase64String(hashBytes), Salt: salt);
    }
    private static string GenerarSaltStatic()
    {
        byte[] randomBytes = new byte[32];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }
}
