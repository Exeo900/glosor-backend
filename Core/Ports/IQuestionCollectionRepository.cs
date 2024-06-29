using Core.Entities;

namespace Core.Ports;
public interface IQuestionCollectionRepository
{
    Task<QuestionCollection?> Get(Guid id);
    Task<IEnumerable<QuestionCollection>> GetAll();
    Task Store(QuestionCollection question);
    Task<bool> Update(QuestionCollection question);
    Task<bool> Delete(Guid id);
}
