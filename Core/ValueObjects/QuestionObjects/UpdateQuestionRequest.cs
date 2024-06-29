namespace Core.ValueObjects.QuestionObjects;
public class UpdateQuestionRequest
{
    public Guid Id { get; set; } = default!;
    public string Text { get; init; } = default!;
    public string AnswerText { get; init; } = default!;
    public int QuestionType { get; init; }
    public string? Description { get; init; }
}
