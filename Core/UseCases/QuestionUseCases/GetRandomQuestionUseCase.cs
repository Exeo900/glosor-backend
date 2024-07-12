using Core.Ports;
using Core.ValueObjects.WordQuestionObjects;
using Serilog;

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
        Log.Information($"Execute {nameof(GetRandomQuestionUseCase)} - Get random question with question collection id {{questionCollectionId}}", questionCollectionId);

        return await _wordQuestionRepository.GetRandom(questionCollectionId);
    }
}
