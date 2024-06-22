using Core.Ports;
using Core.ValueObjects;

namespace Core.UseCases;
public class DeleteQuestionUseCase
{
    private readonly IQuestionRepository _questionRepository;

    public DeleteQuestionUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<bool> Execute(DeleteQuestionRequest deleteQuestionRequest)
    {
        return await _questionRepository.Delete(deleteQuestionRequest.Id);
    }
}
