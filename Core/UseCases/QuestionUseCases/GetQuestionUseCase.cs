using Core.Entities;
using Core.Ports;
using Serilog;

namespace Core.UseCases.QuestionUseCases;
public class GetQuestionUseCase
{
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Question?> Execute(Guid id)
    {
        Log.Information($"Execute {nameof(GetQuestionUseCase)} - Get question with id {{id}}", id);

        return await _questionRepository.Get(id);
    }
}
