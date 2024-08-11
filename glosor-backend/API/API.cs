using Core.UseCases.AuthenticationUseCases;
using Microsoft.AspNetCore.Authorization;

namespace glosor_backend.API;

public static class API
{
    public static void ConfigureApi(this WebApplication webApplicationBuilder)
    {
        webApplicationBuilder.MapGet("/siteInfo", SiteInfo);
        webApplicationBuilder.MapGet("/login", Login);

        webApplicationBuilder.MapGet("/glosorQuestions", QuestionsEndpoints.GetAllQuestionRequests);
        webApplicationBuilder.MapGet("/glosorQuestions/{Id:guid}", QuestionsEndpoints.GetQuestionRequest);
        webApplicationBuilder.MapPost("/glosorQuestions", QuestionsEndpoints.CreateQuestionRequest);
        webApplicationBuilder.MapPut("/glosorQuestions", QuestionsEndpoints.UpdateQuestionRequest);
        webApplicationBuilder.MapDelete("/glosorQuestions", QuestionsEndpoints.DeleteQuestionRequest);

        webApplicationBuilder.MapGet("/glosorQuestions/getRandom/", QuestionsEndpoints.GetRandomQuestion);
        webApplicationBuilder.MapPost("/glosorQuestions/validate/", QuestionsEndpoints.ValidateGuess);

        webApplicationBuilder.MapGet("/glosorQuestionCollections", QuestionsCollectionsEndspoints.GetAllQuestionCollections);
        webApplicationBuilder.MapGet("/glosorQuestionCollections/{Id:guid}", QuestionsCollectionsEndspoints.GetQuestionCollection);
        webApplicationBuilder.MapPost("/glosorQuestionCollections", QuestionsCollectionsEndspoints.CreateQuestionCollection);
    }

    [AllowAnonymous]
    public static IResult SiteInfo(IConfiguration configuration, IWebHostEnvironment env)
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

    [AllowAnonymous]
    public static string Login(GenerateTokenUseCase generateTokenUseCase)
    {
        var generatedToken = generateTokenUseCase.Execute();

        return generatedToken;
    }
}