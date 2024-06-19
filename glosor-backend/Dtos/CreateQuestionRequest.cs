namespace glosor_backend.Dtos;

public class CreateQuestionRequest
{
    public string Text { get; set; }
    public string AnswerText { get; set; }
    public int QuestionTypeId { get; set; }
}

