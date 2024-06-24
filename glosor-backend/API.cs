using Core.Entities;
using Core.Entities.Enums;
using Core.UseCases;
using glosor_backend.Dtos;

namespace glosor_backend;

public static class API
{
    public static void ConfigureApi(this WebApplication webApplicationBuilder)
    {
        webApplicationBuilder.MapGet("/siteInfo", SiteInfo);
        webApplicationBuilder.MapPost("/glosorQuestions", CreateQuestionRequest);
        webApplicationBuilder.MapGet("/glosorQuestions", GetAllQuestionRequests);
        webApplicationBuilder.MapGet("/glosorQuestions/{Id:guid}", GetQuestionRequest);
        webApplicationBuilder.MapPut("/glosorQuestions", UpdateQuestionRequest);
        webApplicationBuilder.MapDelete("/glosorQuestions", DeleteQuestionRequest);
    }

    private static IResult SiteInfo(IConfiguration configuration, IWebHostEnvironment env)
    {
        try
        {
            return Results.Ok($"Hello World! Environment: {env.EnvironmentName}");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

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

       await createQuestionUseCase.Execute(new Core.ValueObjects.CreateQuestionRequest()
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
        return await getAllQuestionsUseCase.Execute(new Core.ValueObjects.GetQuestionRequest() { Id = id });
    }

    public static async Task<bool> UpdateQuestionRequest(UpdateQuestionRequest updateQuestionRequest, UpdateQuestionUseCase updateQuestionUseCase)
    {
        return await updateQuestionUseCase.Execute(new Core.ValueObjects.UpdateQuestionRequest()
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
        return await deleteQuestionUseCase.Execute(new Core.ValueObjects.DeleteQuestionRequest() { Id = id });
    }
}

