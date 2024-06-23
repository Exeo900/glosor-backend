using Microsoft.Data.SqlClient;

namespace Adapter.Persistence.SqlServer.ConnectionFactory;
public interface IConnectionFactory
{
    SqlConnection CreateConnection();
}
