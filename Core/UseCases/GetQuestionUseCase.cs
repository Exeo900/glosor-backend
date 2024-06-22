using Core.Entities;
using Core.Ports;
using Core.ValueObjects;

namespace Core.UseCases;
public class GetQuestionUseCase
{
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Question> Execute(GetQuestionRequest getQuestionRequest)
    {
        return await _questionRepository.Get(getQuestionRequest.Id);
    }
}
