using System.Linq.Expressions;
using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Repository;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate);
    }
    public async Task<T?> GetById(int id)
    {
       return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        _context.AddRange(entities);
       await _context.SaveChangesAsync();
    }

    public async Task<bool> Update(T entity)
    {
      _context.Set<T>().Update(entity);
      var isUpdated = await _context.SaveChangesAsync();
        return isUpdated > 0;
    }

    public async Task<bool> Delete(T entity)
    {
        entity.Deleted = true;
        var isDeleted = await _context.SaveChangesAsync();
        return isDeleted > 0;
    }

    public async Task<bool> HardDelete(T entity)
    {
        _context.Set<T>().Remove(entity);
        var isRemoved = await _context.SaveChangesAsync();
        return isRemoved > 0;
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AnyAsync(predicate);
    }
}
