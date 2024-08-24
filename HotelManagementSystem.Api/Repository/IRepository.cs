using System.Linq.Expressions;
using HotelManagementSystem.Api.Models.Common;

namespace HotelManagementSystem.Api.Repository;

public interface IRepository<T>
{
    IQueryable<T> GetAll();
    IQueryable<T> GetAll(Expression<Func<T,bool>> predicate);
    Task<T?> GetById(int id);

    Task<T> AddAsync(T entity);
    Task AddRange(IEnumerable<T> entities);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
    Task<bool> HardDelete(T entity);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);


}
