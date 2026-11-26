using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Shared.Inferfaces;

namespace Shared.Repository
{
    /// <summary>
    /// Provider-agnostic ADO.NET repository. Uses an injected IDbConnectionFactory to obtain connections.
    /// Mapping from DbDataReader to T is performed by the caller-provided delegate.
    /// </summary>
    public class AdoRepository<T> : IAdoRepository<T>
    {
        private readonly IDbConnectionFactory _factory;

        public AdoRepository(IDbConnectionFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<IEnumerable<T>> QueryAsync(string sql, Func<DbDataReader, T> map, params DbParameter[] parameters)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentException("SQL must be provided", nameof(sql));
            if (map == null) throw new ArgumentNullException(nameof(map));

            using var conn = _factory.CreateConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    if (p != null)
                        cmd.Parameters.Add(p);
                }
            }

            using var reader = await cmd.ExecuteReaderAsync();
            var list = new List<T>();
            while (await reader.ReadAsync())
            {
                list.Add(map(reader));
            }

            return list;
        }

        public async Task<T?> QuerySingleAsync(string sql, Func<DbDataReader, T> map, params DbParameter[] parameters)
        {
            var results = await QueryAsync(sql, map, parameters);
            foreach (var r in results)
                return r;
            return default;
        }

        public async Task<int> ExecuteAsync(string sql, params DbParameter[] parameters)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentException("SQL must be provided", nameof(sql));

            using var conn = _factory.CreateConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    if (p != null)
                        cmd.Parameters.Add(p);
                }
            }

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}
