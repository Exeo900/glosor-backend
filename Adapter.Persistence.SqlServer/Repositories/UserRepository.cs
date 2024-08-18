using Adapter.Persistence.SqlServer.ConnectionFactory;
using Core.Entities;
using Core.Ports;
using Dapper;

namespace Adapter.Persistence.SqlServer.Repositories;
public class UserRepository : IUserRepository
{
    public readonly IConnectionFactory _connectionFactory;

    public UserRepository(IConnectionFactory configuration)
    {
        _connectionFactory = configuration;
    }

    public async Task<User> GetUserByUserNameAndPassword(string userName, string password)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string query = $@"SELECT Id, UserName, Password FROM dbo.[User] where UserName = '{userName}' AND Password = '{password}'";

            return await connection.QueryFirstAsync<User>(query);
        }
    }

    public async Task<User> GetUserByRefreshToken(Guid refreshToken)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string query = $@"SELECT Id, UserName, Password FROM dbo.[User] where RefreshTokenId = '{refreshToken}'";

            return await connection.QueryFirstAsync<User>(query);
        }        
    }

    public async Task<bool> Update(Question question)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            var updateQuery =
                @"UPDATE [dbo].Question SET 
                    Text = @Text, 
                    AnswerText = @AnswerText, 
                    QuestionTypeId = @QuestionTypeId,
                    Description = @Description,
                    Occurrences = @Occurrences,
                    IncorrectAnswers = @IncorrectAnswers
                WHERE Id = @Id";

            var result = await connection.ExecuteAsync(updateQuery, question);

            return result > 0;
        }
    }

    public async void Update(User user)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            var updateQuery =
                @"UPDATE [dbo].[User] 
                SET 
                    UserName = @UserName,
                    Password = @Password,
                    RefreshTokenId = @RefreshTokenId
                WHERE Id = @Id";

            var result = await connection.ExecuteAsync(updateQuery, user);
        }
    }
}
