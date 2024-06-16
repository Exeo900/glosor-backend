namespace Core.Entities;
public class Question : Entity
{
    public string Text { get; set; }
    public string AnswerText { get; set; }
    public int QuestionType { get; set; }
}
