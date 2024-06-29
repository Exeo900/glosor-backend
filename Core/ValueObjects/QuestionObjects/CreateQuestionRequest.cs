namespace Core.ValueObjects.QuestionObjects;

public class CreateQuestionRequest
{
    public string Text { get; init; } = default!;
    public string AnswerText { get; init; } = default!;
    public int QuestionType { get; set; }
    public string? Description { get; set; }
    public Guid QuestionCollectionId { get; set; } = default!;
}
