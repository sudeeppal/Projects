using System.Data.Common;

namespace Shared.Inferfaces
{
    /// <summary>
    /// Factory to create openable DbConnection instances.
    /// Implementations should return a provider-specific DbConnection (SqlConnection, NpgsqlConnection, etc.).
    /// </summary>
    public interface IDbConnectionFactory
    {
        DbConnection CreateConnection();
    }
}
