using Core.Entities;
using Core.Ports;
using Core.ValueObjects.QuestionObjects;

namespace Core.UseCases.QuestionUseCases;
public class UpdateQuestionUseCase
{
    private readonly IQuestionRepository _questionRepository;

    public UpdateQuestionUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<bool> Execute(UpdateQuestionRequest updateQuestionRequest)
    {
        var question = new Question()
        {
            Id = updateQuestionRequest.Id,
            Text = updateQuestionRequest.Text,
            AnswerText = updateQuestionRequest.AnswerText,
            QuestionTypeId = updateQuestionRequest.QuestionType,
        };

        return await _questionRepository.Update(question);
    }
}
