using Core.Entities;
using Core.Ports;

namespace Core.UseCases.QuestionUseCases;
public class GetAllQuestionsCollectionUseCase
{
    private readonly IQuestionCollectionRepository _questionCollectionRepository;

    public GetAllQuestionsCollectionUseCase(IQuestionCollectionRepository questionCollectionRepository)
    {
        _questionCollectionRepository = questionCollectionRepository;
    }

    public async Task<IEnumerable<QuestionCollection>> Execute()
    {
        return await _questionCollectionRepository.GetAll();
    }
}
