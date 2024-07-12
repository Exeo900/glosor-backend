using Core.Ports;
using Core.ValueObjects.QuestionObjects;
using Serilog;

namespace Core.UseCases.QuestionUseCases;
public class DeleteQuestionUseCase
{
    private readonly IQuestionRepository _questionRepository;

    public DeleteQuestionUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<bool> Execute(Guid id)
    {
        Log.Information($"Execute {nameof(DeleteQuestionUseCase)} - Deleting question with id: {{id}}", id);

        return await _questionRepository.Delete(id);
    }
}
