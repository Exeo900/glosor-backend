using Core.Entities;
using Core.Ports;
using Serilog;

namespace Core.UseCases.QuestionUseCases;
public class GetQuestionBySearchTermUseCase
{
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionBySearchTermUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<Question>> Execute(string searchTerm)
    {
        Log.Information($"Execute {nameof(GetQuestionBySearchTermUseCase)}");

        var questions = await _questionRepository.GetByText(searchTerm);

        Log.Information($"{questions.Count()} questions was fetched with search term {searchTerm}");

        return questions;
    }
}
