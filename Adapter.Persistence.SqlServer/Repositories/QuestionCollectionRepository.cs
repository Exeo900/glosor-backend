using Adapter.Persistence.SqlServer.ConnectionFactory;
using Core.Entities;
using Core.Ports;
using Dapper;

namespace Adapter.Persistence.SqlServer.Repositories;
public class QuestionCollectionRepository : IQuestionCollectionRepository
{
    public readonly IConnectionFactory _connectionFactory;

    public QuestionCollectionRepository(IConnectionFactory configuration)
    {
        _connectionFactory = configuration;
    }

    public async Task<QuestionCollection?> Get(Guid id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string query = @"SELECT Id, Name, Description FROM dbo.[QuestionCollection] where ";

            return await connection.QueryFirstAsync<QuestionCollection>(query);
        }
    }

    public async Task<IEnumerable<QuestionCollection>> GetAll()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string query = @"SELECT Id, Name, Description FROM dbo.[QuestionCollection]";

            return await connection.QueryAsync<QuestionCollection>(query);
        }
    }

    public async Task Store(QuestionCollection questionCollection)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string insertQuery = @"
                INSERT INTO [dbo].QuestionCollection  
                (Name, Description)  
                VALUES  
                (@Name, @Description);";

            var result = await connection.ExecuteAsync(insertQuery, questionCollection);
        }
    }

    public async Task<bool> Update(QuestionCollection questionCollection)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            var updateQuery =
                @"UPDATE [dbo].QuestionCollection SET 
                    Name = @Name, 
                    Description = @Description, 
                WHERE Id = @Id";

           var result = await connection.ExecuteAsync(updateQuery, questionCollection);

           return result > 0;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(@"DELETE FROM [dbo].QuestionCollection WHERE Id = @Id", new { Id = id.ToString() });

            return result > 0;
        }
    }
}
