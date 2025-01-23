using System.Linq.Expressions;
using Dominio;
using SGII_Back.Dominio.Entities;

namespace SGII_Back.Dominio.Interfaces.Services;

public interface IUserService
{
    ClsUser? GetById(int? id);
    Task<ClsUser?> GetByEmail(string email);
    IEnumerable<ClsUser> GetAll(Expression<Func<ClsUser, bool>>? filter = null);
    void Create(ClsUser user);
    void Update(ClsUser User);
    void UpdatePassword(ClsUser updatedUser);

}
