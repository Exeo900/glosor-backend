using Core.Ports;
using Core.Entities;
using Core.ValueObjects.QuestionCollectionObjects;

namespace Core.UseCases.QuestionCollectionUseCases;
public class CreateQuestionsCollectionUseCase
{
    private readonly IQuestionCollectionRepository _questionCollectionRepository;

    public CreateQuestionsCollectionUseCase(IQuestionCollectionRepository questionRepository)
    {
        _questionCollectionRepository = questionRepository;
    }

    public async Task<QuestionCollection> Execute(CreateQuestionCollectionsRequest updateQuestionCollectionRequest)
    {
        var questionCollection = new QuestionCollection()
        {
            Name = updateQuestionCollectionRequest.Name,
            Description = updateQuestionCollectionRequest.Description,
        };

        await _questionCollectionRepository.Store(questionCollection);

        return await Task.FromResult(questionCollection);
    }
}
