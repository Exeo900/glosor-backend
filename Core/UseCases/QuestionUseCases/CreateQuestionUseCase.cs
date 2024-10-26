using Core.Entities;
using Core.Entities.Exceptions;
using Core.Ports;
using Core.ValueObjects.QuestionObjects;
using Serilog;

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
        Log.Information($"Execute {nameof(CreateQuestionUseCase)} - New question with Text: {{Text}}", createQuestionRequest.Text);

        var existingQuestions = await _questionRepository.GetByText(createQuestionRequest.Text);

        if (existingQuestions != null && existingQuestions.Any())
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

        Log.Information("Creating question {@question}", question);

        await _questionRepository.Store(question);

        return await Task.FromResult(question);
    }
}
