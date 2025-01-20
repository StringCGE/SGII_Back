using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using SGII_Back.Dominio.Interfaces.Repositories;
using SGII_Back.Dominio.Shared;

namespace Infraestructure.Persistence.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{

    private bool disposedValue;

    public BaseRepository()
    {
    }

    public IQueryable<T> All => throw new NotImplementedException();

    public Task DeleteAsync(T entity)
    {
        return UpdateAsync(entity);
    }
    public Task DeleteDonorPerson(T entity)
    {
        return UpdateAsync(entity);
    }

    public void Detach(T entity)
    {

    }

    public Task DetachAsync(T entity)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public bool Exists(Expression<Func<T, bool>> expression)
    {
        return AsQueryable().Any(expression);
    }

    public Task<bool> ExistsAscyn(Expression<Func<T, bool>> expression)
    {
        return AsQueryable().AnyAsync(expression);
    }

    public T First(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        return PerformInclusions(include).First(expression);
    }

    public Task<T> FirstAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        return PerformInclusions(include).FirstAsync(expression);
    }
    public T? FirstOrDefault(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        return PerformInclusions(include).FirstOrDefault(expression);
    }

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        return PerformInclusions(include).FirstOrDefaultAsync(expression);
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        return expression is null ? PerformInclusions(include).AsEnumerable()
            :
            PerformInclusions(include).Where(expression).AsEnumerable();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = PerformInclusions(include);
        return expression is null ? await query.ToListAsync() : await query.Where(expression).ToListAsync();
    }

    public IQueryable<T> GetAllQuerable(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = PerformInclusions(include);
        return expression is null ? query : query.Where(expression);
    }

    public async Task<IQueryable<T>> GetAllQueryableAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        return null;
    }

    public T? GetById(params object[] keyValues)
    {
        return null;
    }

    public async Task<T?> GetByIdAsync(params object[] keyValues)
    {
        return null;
    }

    public void Insert(T entity)
    {
        
    }

    public async Task InsertAsync(T entity)
    {
       
    }

    public IQueryable<T> OrderBy(params Expression<Func<T, object>>[] properties)
    {
        return null;
    }

    public IQueryable<T> OrderByDesc(params Expression<Func<T, object>>[] properties)
    {
        IQueryable<T> query = All;
        foreach (Expression<Func<T, object>> property in properties)
        {
            query = All.OrderByDescending(property);
        }
        return query;
    }

    public void SaveChanges(IOperationRequest? request = null)
    {

    }

    public async Task SaveChangesAsync(IOperationRequest? request = null)
    {

    }

    public async Task SaveChangesAsync()
    {
    }

    public IOperationResultList<T> ToResultListPaginated(
                     int Offset = 1,
                        int? Take = 10)
    {
        return null;
    }

    public IOperationResultList<TResult> ToResultListPaginated<TResult>(
         int Offset = 1,
                        int Take = 10) where TResult : class
    {
        return null;
    }

    public Task<IOperationResultList<TResult>> ToResultListPaginatedAscyn<TResult>(int Offset = 1, int Take = 10) where TResult : class
    {
        return null;
    }



    public void Update(T entity)
    {
    }

    public Task UpdateAsync(T entity)
    {
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: eliminar el estado administrado (objetos administrados)
            }

            // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
            // TODO: establecer los campos grandes como NULL
            disposedValue = true;
        }
    }

    private IQueryable<T> AsQueryable()
    {
        return null;
    }

    private IQueryable<T> PerformInclusions(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include)
    {
        return include == null ? AsQueryable() : include(AsQueryable());
    }

    private void SetDatabefore(IOperationRequest request)
    {
    }
}
