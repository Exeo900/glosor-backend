using Core.Entities;
using Core.Ports;

namespace Core.UseCases.QuestionUseCases;
public class GetQuestionsCollectionUseCase
{
    private readonly IQuestionCollectionRepository _questionCollectionRepository;

    public GetQuestionsCollectionUseCase(IQuestionCollectionRepository questionCollectionRepository)
    {
        _questionCollectionRepository = questionCollectionRepository;
    }

    public async Task<QuestionCollection?> Execute(Guid id)
    {
        return await _questionCollectionRepository.Get(id);
    }
}
