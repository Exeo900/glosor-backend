namespace glosor_backend.Dtos;

public class CreateQuestionsCollectionRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}