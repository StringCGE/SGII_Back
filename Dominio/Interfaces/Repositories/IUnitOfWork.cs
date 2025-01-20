namespace SGII_Back.Dominio.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{

    Task SaveChangesAsync();
    void SaveChanges();

}
