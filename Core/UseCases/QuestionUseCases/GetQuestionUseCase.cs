using Core.Entities;
using Core.Ports;
using Core.ValueObjects.QuestionObjects;

namespace Core.UseCases.QuestionUseCases;
public class GetQuestionUseCase
{
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Question?> Execute(GetQuestionRequest getQuestionRequest)
    {
        return await _questionRepository.Get(getQuestionRequest.Id);
    }
}
