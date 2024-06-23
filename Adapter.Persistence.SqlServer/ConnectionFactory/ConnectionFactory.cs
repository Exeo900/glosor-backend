using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Adapter.Persistence.SqlServer.ConnectionFactory;
public class ConnectionFactory : IConnectionFactory
{
    public readonly IConfiguration _configuration;

    public ConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("Default"));
    }
}
