using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Inferfaces;

namespace Shared.Repository;

public class EntityFrameworkRepository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;

    public EntityFrameworkRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>().AsQueryable();
    }

    public async Task<T?> GetByIdAsync(params object[] keyValues)
    {
        if (keyValues == null || keyValues.Length == 0)
            throw new ArgumentException("Key values must be provided", nameof(keyValues));

        var entity = await _context.Set<T>().FindAsync(keyValues);
        return entity;
    }

    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AnyAsync(predicate);
    }
}
