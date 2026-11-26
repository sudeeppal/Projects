using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Shared.Inferfaces
{
    /// <summary>
    /// Lightweight ADO.NET repository interface for executing SQL and mapping results.
    /// </summary>
    public interface IAdoRepository<T>
    {
        Task<IEnumerable<T>> QueryAsync(string sql, Func<DbDataReader, T> map, params DbParameter[] parameters);

        Task<T?> QuerySingleAsync(string sql, Func<DbDataReader, T> map, params DbParameter[] parameters);

        Task<int> ExecuteAsync(string sql, params DbParameter[] parameters);
    }
}
