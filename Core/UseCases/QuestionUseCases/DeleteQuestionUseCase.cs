using Core.Ports;

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
        return await _questionRepository.Delete(id);
    }
}
