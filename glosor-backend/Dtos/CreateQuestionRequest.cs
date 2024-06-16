namespace glosor_backend.Dtos;

public class CreateQuestionRequest
{
    public string Text { get; set; }
    public string AnswerText { get; set; }
    public int QuestionType { get; set; }
}

