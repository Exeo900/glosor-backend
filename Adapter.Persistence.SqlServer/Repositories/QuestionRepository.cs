using Core.Entities;
using Core.Ports;

namespace Adapter.Persistence.SqlServer.Repositories;
public class QuestionRepository : IQuestionRepository
{
    public Task<Question> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Store(Question question)
    {
        throw new NotImplementedException();
    }
}
