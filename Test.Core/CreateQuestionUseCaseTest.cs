using Core.Entities;
using Core.Entities.Enums;
using Core.Entities.Exceptions;
using Core.Ports;
using Core.UseCases.QuestionUseCases;
using Core.ValueObjects.QuestionObjects;
using Moq;

namespace Test.Core;

public class CreateQuestionUseCaseTest
{
    private readonly CreateQuestionUseCase sut;
    private readonly Mock<IQuestionRepository> questionRepositoryMock;
    private readonly Question Question;

    public CreateQuestionUseCaseTest()
    {
        Question = new Question()
        {
            Text = "Funicular",
            AnswerText = "Bergbana",
            QuestionTypeId = (int) QuestionType.Noun
        };

        questionRepositoryMock = new Mock<IQuestionRepository>();
        questionRepositoryMock.Setup(x => x.GetByText(Question.Text)).ReturnsAsync(Question);

        sut = new CreateQuestionUseCase(questionRepositoryMock.Object);
    }

    [Fact]
    public async void A_new_question_can_be_created()
    {
        var createQuestionRequest = new CreateQuestionRequest()
        {
            Text = "Gossamer",
            AnswerText = "Spindelväv",
            QuestionTypeId = (int) QuestionType.Noun
        };

        await sut.Execute(createQuestionRequest);

        questionRepositoryMock.Verify(x => x.Store(It.IsAny<Question>()), Times.Once);
    }

    [Fact]
    public async void A_new_question_with_an_existing_text_can_not_be_created()
    {
        var createQuestionRequest = new CreateQuestionRequest()
        {
            Text = Question.Text,
            AnswerText = Question.AnswerText,
            QuestionTypeId = Question.QuestionTypeId
        };

        await Assert.ThrowsAsync<DuplicateQuestionException>(async () =>
             await sut.Execute(createQuestionRequest)
        );
    }
}
