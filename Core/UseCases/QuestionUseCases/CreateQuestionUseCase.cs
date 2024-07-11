using Core.Entities;
using Core.Entities.Exceptions;
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
        var existingQuestion = await _questionRepository.GetByText(createQuestionRequest.Text);

        if (existingQuestion != null)
        {
            throw new DuplicateQuestionException($"Question with text '{createQuestionRequest.Text}' already exists");
        }

        var question = new Question()
        {
            Text = createQuestionRequest.Text,
            AnswerText = createQuestionRequest.AnswerText,
            QuestionTypeId = createQuestionRequest.QuestionTypeId,
            Description = createQuestionRequest.Description,
            QuestionCollectionId = createQuestionRequest.QuestionCollectionId
        };

        await _questionRepository.Store(question);

        return await Task.FromResult(question);
    }
}
