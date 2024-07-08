using Adapter.Persistence.SqlServer.ConnectionFactory;
using Core.Entities;
using Core.Ports;
using Core.ValueObjects.WordQuestionObjects;
using Dapper;

namespace Adapter.Persistence.SqlServer.Repositories;
public class WordQuestionRepository : IWordQuestionRepository
{
    public readonly IConnectionFactory _connectionFactory;

    public WordQuestionRepository(IConnectionFactory configuration)
    {
        _connectionFactory = configuration;
    }

    public async Task<WordQuestionData?> GetRandom(Guid questionCollectionId)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            string selectQuery = $@"WITH Top20Questions AS (
                    SELECT TOP 20 
                            'x' as 'Split1', q.Id AS QuestionId, q.Text, q.AnswerText, q.QuestionTypeId, q.Description AS QuestionDescription, q.CreatedDate, q.Occurrences, q.IncorrectAnswers, q.QuestionCollectionId, 
                            'x' as 'Split2', qc.Id AS CollectionId, qc.Name AS CollectionName, qc.Description AS CollectionDescription 
                    FROM [dbo].Question q
                    INNER JOIN [dbo].QuestionCollection qc ON q.[QuestionCollectionId] = qc.Id 
                    WHERE q.[QuestionCollectionId] = '{questionCollectionId}'
                    ORDER BY q.Occurrences ASC
                ),
                RandomQuestion AS (
                    SELECT TOP 1 *
                    FROM Top20Questions
                    ORDER BY NEWID()
                )
                SELECT  rq.Split1, rq.QuestionId as Id, rq.Text, rq.AnswerText, rq.QuestionTypeId, rq.QuestionDescription as Description, rq.CreatedDate, rq.Occurrences, rq.IncorrectAnswers, rq.QuestionCollectionId, 
                        rq.Split2, rq.CollectionId as Id , rq.CollectionName as Name, rq.CollectionDescription as Description, 
                        (SELECT STRING_AGG(a.AnswerText, ';; ') 
                            FROM (
                                SELECT TOP 4 a.AnswerText
                                FROM [dbo].Question a
                                WHERE a.QuestionCollectionId = '{questionCollectionId}' AND a.Id != rq.QuestionId AND a.QuestionTypeId = rq.QuestionTypeId
                                ORDER BY NEWID()
                            ) AS a
                        ) AS Alternatives
                FROM RandomQuestion rq;";

            var result = await connection.QueryAsync<WordQuestionData, Question, QuestionCollection, string, WordQuestionData>(selectQuery,
                (wordQuestionData, question, questionCollection, alternatives) =>
                {
                    wordQuestionData.Question = question;
                    wordQuestionData.Question.QuestionCollection = questionCollection;
                    wordQuestionData.Alternatives = alternatives.Split(new[] { ";; " }, StringSplitOptions.None);
                    return wordQuestionData;
                },
                splitOn: "Id,Id,Alternatives");

            return result.FirstOrDefault();
        }
    }
}


