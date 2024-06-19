using Core.Entities.Enums;

namespace Core.Entities;
public class Question : Entity
{
    public string Text { get; set; }
    public string AnswerText { get; set; }
    public int QuestionTypeId { get; set; }
    public QuestionType QuestionType
    {
        get { return (QuestionType) QuestionTypeId; }
        set { QuestionTypeId = (int) value; }
    }
}
