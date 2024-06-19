using Core.Entities;
using Core.Entities.Enums;
using Core.UseCases;
using glosor_backend.Dtos;
using System;

namespace glosor_backend;

public static class API
{
    public static void ConfigureApi(this WebApplication webApplicationBuilder)
    {
        webApplicationBuilder.MapGet("siteInfo", SiteInfo);
        webApplicationBuilder.MapPost("glosorQuestion", CreateQuestionRequest);
        webApplicationBuilder.MapGet("glosorQuestion", GetAllQuestionRequests);
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
            QuestionType = createQuestionRequest.QuestionTypeId
        });

        return Results.Ok("Created");
    }

    public static async Task<IEnumerable<Question>> GetAllQuestionRequests(GetAllQuestionsUseCase getAllQuestionsUseCase)
    {
        return await getAllQuestionsUseCase.Execute();
    }
}

