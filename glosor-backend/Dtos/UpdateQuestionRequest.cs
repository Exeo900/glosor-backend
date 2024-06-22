namespace glosor_backend.Dtos;

public class UpdateQuestionRequest
{
    public Guid Id { get; init; }
    public string Text { get; init; } = default!;
    public string AnswerText { get; init; } = default!;
    public int QuestionTypeId { get; init; }
}