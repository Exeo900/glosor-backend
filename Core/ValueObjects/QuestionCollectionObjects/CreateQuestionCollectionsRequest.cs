namespace Core.ValueObjects.QuestionCollectionObjects;
public class CreateQuestionCollectionsRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
