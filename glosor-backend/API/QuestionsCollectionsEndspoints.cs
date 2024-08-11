using Core.Entities;
using Core.UseCases.QuestionCollectionUseCases;
using Core.UseCases.QuestionUseCases;
using glosor_backend.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace glosor_backend.API;

public static class QuestionsCollectionsEndspoints
{
    [Authorize]
    public static async Task<IResult> GetAllQuestionCollections(GetAllQuestionsCollectionUseCase getAllQuestionsCollectionUseCase)
    {
        var result = await getAllQuestionsCollectionUseCase.Execute();

        return Results.Ok(result);
    }

    [Authorize]
    public static async Task<QuestionCollection?> GetQuestionCollection(GetQuestionsCollectionUseCase getQuestionsCollectionUseCase, Guid id)
    {
        return await getQuestionsCollectionUseCase.Execute(id);
    }

    [Authorize]
    public static async Task<IResult> CreateQuestionCollection(CreateQuestionsCollectionUseCase createQuestionsCollectionUseCase, CreateQuestionsCollectionRequest createQuestionsCollectionRequest)
    {
        if (createQuestionsCollectionRequest == null)
        {
            return Results.BadRequest();
        }

        await createQuestionsCollectionUseCase.Execute(new Core.ValueObjects.QuestionCollectionObjects.CreateQuestionCollectionsRequest()
        {
            Name = createQuestionsCollectionRequest.Name,
            Description = createQuestionsCollectionRequest.Description,
        });

        return Results.Ok("Created");
    }

    [Authorize]
    public static async Task<bool> UpdateQuestionRequest(UpdateQuestionsCollectionUseCase updateQuestionCollectionUseCase, UpdateQuestionCollectionRequest updateQuestionCollectionRequest)
    {
        return await updateQuestionCollectionUseCase.Execute(new Core.ValueObjects.QuestionCollectionObjects.UpdateQuestionCollectionRequest()
        {
            Id = updateQuestionCollectionRequest.Id,
            Name = updateQuestionCollectionRequest.Name,
            Description = updateQuestionCollectionRequest.Description
        });
    }
}
