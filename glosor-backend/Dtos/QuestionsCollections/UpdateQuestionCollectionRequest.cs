namespace glosor_backend.Dtos;

public class UpdateQuestionCollectionRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}