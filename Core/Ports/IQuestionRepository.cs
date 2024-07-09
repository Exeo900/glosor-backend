using Core.Entities;

namespace Core.Ports;
public interface IQuestionRepository
{
    Task<Question?> Get(Guid id);
    Task<IEnumerable<Question>> GetAll();
    Task Store(Question question);
    Task<bool> Update(Question question);
    Task<bool> Delete(Guid id);
    Task<Question?> GetByText(string text);
}
