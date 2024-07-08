using Core.Entities.Enums;
using Core.Entities;
using glosor_backend.Dtos;
using Core.UseCases.QuestionUseCases;
using Core.ValueObjects.WordQuestionObjects;

namespace glosor_backend.API;

public static class QuestionsEndpoints 
{
    public static async Task<IResult> CreateQuestionRequest(CreateQuestionUseCase createQuestionUseCase, CreateQuestionRequest createQuestionRequest)
    {
        if (createQuestionRequest == null)
        {
            return Results.BadRequest();
        }

        if (!Enum.IsDefined(typeof(QuestionType), createQuestionRequest.QuestionTypeId))
        {
            return Results.BadRequest($"Question type does not exists: {createQuestionRequest.QuestionTypeId}");
        }

        await createQuestionUseCase.Execute(new Core.ValueObjects.QuestionObjects.CreateQuestionRequest()
        {
            Text = createQuestionRequest.Text,
            AnswerText = createQuestionRequest.AnswerText,
            QuestionType = createQuestionRequest.QuestionTypeId,
            Description = createQuestionRequest.Description,
            QuestionCollectionId = createQuestionRequest.QuestionCollectionId
        });

        return Results.Ok("Created");
    }

    public static async Task<IEnumerable<Question>> GetAllQuestionRequests(GetAllQuestionsUseCase getAllQuestionsUseCase)
    {
        return await getAllQuestionsUseCase.Execute();
    }

    public static async Task<Question?> GetQuestionRequest(GetQuestionUseCase getAllQuestionsUseCase, Guid id)
    {
        return await getAllQuestionsUseCase.Execute(new Core.ValueObjects.QuestionObjects.GetQuestionRequest() { Id = id });
    }

    public static async Task<bool> UpdateQuestionRequest(UpdateQuestionRequest updateQuestionRequest, UpdateQuestionUseCase updateQuestionUseCase)
    {
        return await updateQuestionUseCase.Execute(new Core.ValueObjects.QuestionObjects.UpdateQuestionRequest()
        {
            Id = updateQuestionRequest.Id,
            Text = updateQuestionRequest.Text,
            AnswerText = updateQuestionRequest.AnswerText,
            QuestionType = updateQuestionRequest.QuestionTypeId,
            Description = updateQuestionRequest.Description
        });
    }

    public static async Task<bool> DeleteQuestionRequest(DeleteQuestionUseCase deleteQuestionUseCase, Guid id)
    {
        return await deleteQuestionUseCase.Execute(new Core.ValueObjects.QuestionObjects.DeleteQuestionRequest() { Id = id });
    }

    public static async Task<WordQuestionData?> GetRandomQuestion(GetRandomQuestionUseCase getRandomQuestionUseCase, Guid questionCollectionId)
    {
        return await getRandomQuestionUseCase.Execute(questionCollectionId);
    }
}
