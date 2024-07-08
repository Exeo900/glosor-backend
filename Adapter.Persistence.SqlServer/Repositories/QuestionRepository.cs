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

    public async Task<Question?> Get(Guid id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string query = $@"
                SELECT q.Id, q.Text, q.AnswerText, q.QuestionTypeId, q.Description, q.CreatedDate, q.Occurrences, q.IncorrectAnswers, q.QuestionCollectionId, qc.Id, qc.Name, qc.Description 
                FROM [dbo].Question q
                INNER JOIN [dbo].QuestionCollection qc ON q.[QuestionCollectionId] = qc.Id 
                WHERE q.Id = '{id}'";

            var result = await connection.QueryAsync<Question, QuestionCollection, Question>(query, (question, questionCollection) =>
            {
                question.QuestionCollection = questionCollection;
                return question;
            });

            return result.SingleOrDefault();
        }
    }

    public async Task<IEnumerable<Question>> GetAll()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string query = @"
                SELECT q.Id, q.Text, q.AnswerText, q.QuestionTypeId, q.Description, q.CreatedDate, q.Occurrences, q.IncorrectAnswers, q.QuestionCollectionId, qc.Id, qc.Name, qc.Description 
                FROM [dbo].Question q
                INNER JOIN [dbo].QuestionCollection qc ON q.[QuestionCollectionId] = qc.Id
                ORDER BY q.CreatedDate desc";

            return await connection.QueryAsync<Question, QuestionCollection, Question>(query, (question, questionCollection) =>
            {
                question.QuestionCollection = questionCollection;
                return question;
            });
        }
    }

    public async Task Store(Question question)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.OpenAsync();

            string insertQuery = "" +
                "INSERT INTO [dbo].Question " +
                "(Text, AnswerText, QuestionTypeId, Description, QuestionCollectionId) " +
                "VALUES " +
                "(@Text, @AnswerText, @QuestionTypeId, @Description, @QuestionCollectionId);";

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
                    QuestionTypeId = @QuestionTypeId,
                    Description = @Description,
                    Occurrences = @Occurrences,
                    IncorrectAnswers = @IncorrectAnswers
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
