using Core.Entities;
using Core.Entities.Enums;
using Core.Ports;
using Core.UseCases.QuestionUseCases;
using Moq;

namespace Test.Core;

public class ValidateQuestionGuessUseCaseTest
{
    private readonly ValidateQuestionGuessUseCase sut;
    private readonly Mock<IQuestionRepository> questionRepositoryMock;
    private readonly Question Question;
    private Guid QuestionId;

    public ValidateQuestionGuessUseCaseTest()
    {
        QuestionId = Guid.NewGuid();

        Question = new Question()
        {
            Id = QuestionId,
            Text = "Funicular",
            AnswerText = "Bergbana",
            QuestionTypeId = (int) QuestionType.Noun
        };

        questionRepositoryMock = new Mock<IQuestionRepository>();
        questionRepositoryMock.Setup(x => x.Get(QuestionId)).ReturnsAsync(Question);

        sut = new ValidateQuestionGuessUseCase(questionRepositoryMock.Object);
    }

    [Fact]
    public async void Validate_question_with_the_correct_answer_resulting_in_a_successful_validation()
    {
        bool successfulValidation = await sut.Execute(QuestionId, "Bergbana");

        Assert.True(successfulValidation);
    }

    [Fact]
    public async void Validate_question_with_the_incorrect_answer_resulting_in_a_unsuccessful_validation()
    {
        bool successfulValidation = await sut.Execute(QuestionId, "Hoppborg");

        Assert.False(successfulValidation);
    }
}
