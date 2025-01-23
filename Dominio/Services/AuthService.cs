using System.Security.Cryptography;
using System.Text;
using Dominio;
using Dominio.DB;
using SGII_Back.Dominio.Entities;
using SGII_Back.Dominio.Interfaces.Repositories;
using SGII_Back.Dominio.Interfaces.Services;

namespace SGII_Back.Dominio.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository usuarioRepository)
    {
        _userRepository = usuarioRepository;
    }

    public async Task<bool> Autenticate(int? userId, string password)
    {
        //ClsUser? _user = _userRepository.GetById(userId);
        DbUser db_user = new DbUser();
        ClsUser? _user = await db_user.ObtenerPorIdAsync(userId ?? 0);
        if (_user != null)
        {
            if (VerifyPassword(password, _user.password, _user.salt))
            {
                await new DbUser().ResetIntento(_user);
                return true;
            }
            else
            {
                await new DbUser().SumarIntento(_user);
                return false;
            }
            
            //object ooo = CreateHash(password);//Para obtener hash y salt para almacenar el primer usuario en la base de datos
            
        }
        return false;
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

    public async Task<int> Intentos(int? userId)//Si Si no se considera las llamadas a la db peor el apuro
    {
        DbUser db_user = new DbUser();
        ClsUser? _user = await db_user.ObtenerPorIdAsync(userId ?? 0);
        if (_user != null)
        {
            return _user.intentos ?? -1;
        }
        return -1;
    }
}
