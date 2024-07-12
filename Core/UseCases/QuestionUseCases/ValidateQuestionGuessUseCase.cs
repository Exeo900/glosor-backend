using Core.Ports;
using Serilog;

namespace Core.UseCases.QuestionUseCases;
public class ValidateQuestionGuessUseCase
{
    private readonly IQuestionRepository _questionRepository;

    public ValidateQuestionGuessUseCase(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<bool> Execute(Guid questionId, string guess)
    {
        Log.Information($"Execute {nameof(ValidateQuestionGuessUseCase)} - validate question with id {questionId} with guess '{guess}'");

        var question = await _questionRepository.Get(questionId);

        if (question == null)
        {
            throw new KeyNotFoundException($"Question with id {questionId} not found!");
        }

        bool correctAnswer = question.AnswerText.Equals(guess);

        if (!correctAnswer)
        {
            question.IncorrectAnswers++;
        }

        question.Occurrences++;

        await _questionRepository.Update(question);

        return correctAnswer;
    }
}
