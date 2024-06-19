using Core.Entities;
using Core.Ports;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Adapter.Persistence.SqlServer.Repositories;
public class QuestionRepository : IQuestionRepository
{
    public readonly IConfiguration _configuration;

    public QuestionRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Question> Get(Guid id)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            await connection.OpenAsync();

            string query = $"SELECT Id, Text, AnswerText, QuestionTypeId FROM Question WHERE Id = '{id}'";

            var question = await connection.QueryFirstAsync<Question>(query);

            return question;
        }
    }

    public async Task<IEnumerable<Question>> GetAll()
    {
        var connectionString = _configuration.GetConnectionString("Default");

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT Id, Text, AnswerText, QuestionTypeId FROM [dbo].Question";

            return await connection.QueryAsync<Question>(query);
        }
    }

    public async Task Store(Question question)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            await connection.OpenAsync();

            string insertQuery = "INSERT INTO [dbo].Question (Text, AnswerText, QuestionTypeId) VALUES (@Text, @AnswerText, @QuestionTypeId);";

            var result = await connection.ExecuteAsync(insertQuery, question);
        }
    }
}
