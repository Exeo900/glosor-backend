using Core.ValueObjects.WordQuestionObjects;

namespace Core.Ports;
public interface IWordQuestionRepository
{
    Task<WordQuestionData?> GetRandom(Guid questionCollectionId);
}
