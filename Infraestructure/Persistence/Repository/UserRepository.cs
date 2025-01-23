
using Infraestructure.Persistence.Repository;
using SGII_Back.Dominio.Entities;
using SGII_Back.Dominio.Interfaces.Repositories;

namespace Dominio.Infrastructure.Persistence.Repository;

public class UserRepository : BaseRepository<ClsUser>, IUserRepository
{

    public UserRepository() :
        base()
    {

    }


    //public User? GetById(int userId)
    //{
    //    this.First(a => a.Id == userId);
    //    return _context.User.Find(userId);
    //}

    //public IEnumerable<User> GetAll()
    //{
    //    return _context.User.ToList();
    //}

    //public void Create(User newUser)
    //{
    //    _context.User.Add(newUser);
    //    _context.SaveChanges();
    //}

    //public void Update(int userId, User updatedUser)
    //{
    //    var usuarioExistente = _context.User.Find(userId);
    //    if (usuarioExistente != null)
    //    {
    //        usuarioExistente.Name = updatedUser.Name;
    //        usuarioExistente.Email = updatedUser.Email;
    //        // Actualizar otros campos según sea necesario
    //        _context.SaveChanges();
    //    }
    //}

    //public void Delete(int userId)
    //{
    //    var usuarioExistente = _context.User.Find(userId);
    //    if (usuarioExistente != null)
    //    {
    //        _context.User.Remove(usuarioExistente);
    //        _context.SaveChanges();
    //    }
    //}
}
