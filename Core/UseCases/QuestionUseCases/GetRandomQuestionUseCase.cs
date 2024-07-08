using Core.Ports;
using Core.ValueObjects.WordQuestionObjects;

namespace Core.UseCases.QuestionUseCases;
public class GetRandomQuestionUseCase
{
    private readonly IWordQuestionRepository _wordQuestionRepository;

    public GetRandomQuestionUseCase(IWordQuestionRepository wordQuestionRepository)
    {
        _wordQuestionRepository = wordQuestionRepository;
    }

    public async Task<WordQuestionData?> Execute(Guid questionCollectionId)
    {
        return await _wordQuestionRepository.GetRandom(questionCollectionId);
    }
}
