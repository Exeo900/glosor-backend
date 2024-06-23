using Adapter.Persistence.SqlServer.ConnectionFactory;
using Core.Entities;
using Core.Ports;
using Dapper;

namespace Adapter.Persistence.SqlServer.Repositories;
public class QuestionRepository : IQuestionRepository
{
    public readonly IConnectionFactory _connectionFactory;

    public QuestionRepository(IConnectionFactory configuration)
    {
        _connectionFactory = configuration;
    }

    public async Task<Question> Get(Guid id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string query = $"SELECT Id, Text, AnswerText, QuestionTypeId, Description, CreatedDate, Occurrences, IncorrectAnswers FROM Question WHERE Id = '{id}'";

            var question = await connection.QueryFirstAsync<Question>(query);

            return question;
        }
    }

    public async Task<IEnumerable<Question>> GetAll()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string query = "SELECT Id, Text, AnswerText, QuestionTypeId, Description, CreatedDate, Occurrences, IncorrectAnswers FROM [dbo].Question";

            return await connection.QueryAsync<Question>(query);
        }
    }

    public async Task Store(Question question)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string insertQuery = "" +
                "INSERT INTO [dbo].Question " +
                "(Text, AnswerText, QuestionTypeId, Description) " +
                "VALUES " +
                "(@Text, @AnswerText, @QuestionTypeId, @Description);";

            var result = await connection.ExecuteAsync(insertQuery, question);
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
                    QuestionTypeId = @QuestionTypeId 
                WHERE Id = @Id";

           var result = await connection.ExecuteAsync(updateQuery, question);

           return result > 0;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(@"DELETE FROM [dbo].Question WHERE Id = @Id", new { Id = id.ToString() });

            return result > 0;
        }
    }
}
