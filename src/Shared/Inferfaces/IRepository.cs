using System.Linq.Expressions;

namespace Shared.Inferfaces;

/// <summary>
/// Generic repository interface for EF Core-backed entities.
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IRepository<T> where T : class
{
    IQueryable<T> Query();

    Task<T?> GetByIdAsync(params object[] keyValues);

    Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate);

    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task AddAsync(T entity);

    void Update(T entity);

    void Remove(T entity);

    Task<int> SaveChangesAsync();

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
}
