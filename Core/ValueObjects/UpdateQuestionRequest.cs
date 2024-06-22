namespace Core.ValueObjects;
public class UpdateQuestionRequest
{
    public Guid Id { get; set; } = default!;
    public string Text { get; init; } = default!;
    public string AnswerText { get; init; } = default!;
    public int QuestionType { get; init; }
}
