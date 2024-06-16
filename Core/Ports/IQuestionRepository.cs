using Core.Entities;

namespace Core.Ports;
public interface IQuestionRepository
{
    Task<Question> Get(Guid id);
    Task Store(Question question);
}
