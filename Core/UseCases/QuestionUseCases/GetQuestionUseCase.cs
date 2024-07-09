using Core.Entities;
using Core.Ports;

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
        return await _questionRepository.Get(id);
    }
}
