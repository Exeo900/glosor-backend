using Core.Entities.Enums;

namespace Core.Entities;
public class Question : Entity
{
    public string Text { get; init; } = default!;
    public string AnswerText { get; init; } = default!;
    public int QuestionTypeId { get; set; }
    public QuestionType QuestionType
    {
        get { return (QuestionType) QuestionTypeId; }
        set { QuestionTypeId = (int) value; }
    }
}
