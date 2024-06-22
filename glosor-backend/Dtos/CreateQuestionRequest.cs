namespace glosor_backend.Dtos;

public class CreateQuestionRequest
{
    public string Text { get; set; } = default!;
    public string AnswerText { get; set; } = default!;
    public int QuestionTypeId { get; set; }
}