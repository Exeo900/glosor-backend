using Core.Entities;
using Core.Ports;
using Core.ValueObjects.QuestionObjects;

namespace Core.UseCases.QuestionUseCases;
public class CreateQuestionUseCase
{
    private readonly IQuestionRepository _questionRepository;

    public CreateQuestionUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Question> Execute(CreateQuestionRequest createQuestionRequest)
    {
        var question = new Question()
        {
            Text = createQuestionRequest.Text,
            AnswerText = createQuestionRequest.AnswerText,
            QuestionTypeId = createQuestionRequest.QuestionType,
            Description = createQuestionRequest.Description,
            QuestionCollectionId = createQuestionRequest.QuestionCollectionId
        };

        await _questionRepository.Store(question);

        return await Task.FromResult(question);
    }
}
