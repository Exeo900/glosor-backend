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
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Occurrences { get; set; }
    public int IncorrectAnswers { get; set; }
    public Guid QuestionCollectionId { get; set; }
    public QuestionCollection QuestionCollection { get; set; } = default!;
}
