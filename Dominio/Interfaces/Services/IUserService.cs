using System.Linq.Expressions;
using SGII_Back.Dominio.Entities;

namespace SGII_Back.Dominio.Interfaces.Services;

public interface IUserService
{
    User? GetById(int id);
    User? GetByEmail(string email);
    IEnumerable<User> GetAll(Expression<Func<User, bool>>? filter = null);
    void Create(User user);
    void Update(User User);
    void UpdatePassword(User updatedUser);

}
