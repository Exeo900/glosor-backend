using Core.Entities;
using Core.Ports;

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
        return await _questionRepository.GetAll();
    }
}
