using Core.Entities;
using Core.Ports;
using Core.ValueObjects.QuestionCollectionObjects;

namespace Core.UseCases.QuestionCollectionUseCases;
public class UpdateQuestionsCollectionUseCase
{
    private readonly IQuestionCollectionRepository _questionCollectionRepository;

    public UpdateQuestionsCollectionUseCase(IQuestionCollectionRepository questionRepository)
    {
        _questionCollectionRepository = questionRepository;
    }

    public async Task<bool> Execute(UpdateQuestionCollectionRequest createQuestionRequest)
    {
        var questionCollection = new QuestionCollection()
        {
            Name = createQuestionRequest.Name
        };

       return await _questionCollectionRepository.Update(questionCollection);
    }
}
