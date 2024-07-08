using Core.Entities;

namespace Core.ValueObjects.WordQuestionObjects;
public class WordQuestionData
{
    public required Question Question { get; set; }
    public required string[] Alternatives { get; set; }
}
