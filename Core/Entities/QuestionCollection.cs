namespace Core.Entities;
public class QuestionCollection : Entity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
