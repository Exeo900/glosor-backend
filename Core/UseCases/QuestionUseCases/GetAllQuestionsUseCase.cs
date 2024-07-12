using Core.Entities;
using Core.Ports;
using Serilog;

namespace Core.UseCases.QuestionUseCases;
public class GetAllQuestionsUseCase
{
    private readonly IQuestionRepository _questionRepository;

    public GetAllQuestionsUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<Question>> Execute()
    {
        Log.Information($"Execute {nameof(GetAllQuestionsUseCase)}");

        var questions = await _questionRepository.GetAll();

        Log.Information($"{questions.Count()} questions was fetched");

        return questions;
    }
}
