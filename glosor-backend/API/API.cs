namespace glosor_backend.API;

public static class API
{
    public static void ConfigureApi(this WebApplication webApplicationBuilder)
    {
        webApplicationBuilder.MapGet("/siteInfo", SiteInfo);

        webApplicationBuilder.MapGet("/glosorQuestions", QuestionsEndpoints.GetAllQuestionRequests);
        webApplicationBuilder.MapGet("/glosorQuestions/{Id:guid}", QuestionsEndpoints.GetQuestionRequest);
        webApplicationBuilder.MapGet("/glosorQuestions/getRandom/", QuestionsEndpoints.GetRandomQuestion);
        webApplicationBuilder.MapPost("/glosorQuestions", QuestionsEndpoints.CreateQuestionRequest);       
        webApplicationBuilder.MapPut("/glosorQuestions", QuestionsEndpoints.UpdateQuestionRequest);
        webApplicationBuilder.MapDelete("/glosorQuestions", QuestionsEndpoints.DeleteQuestionRequest);

        webApplicationBuilder.MapGet("/glosorQuestionCollections", QuestionsCollectionsEndspoints.GetAllQuestionCollections);
        webApplicationBuilder.MapGet("/glosorQuestionCollections/{Id:guid}", QuestionsCollectionsEndspoints.GetQuestionCollection);
        webApplicationBuilder.MapPost("/glosorQuestionCollections", QuestionsCollectionsEndspoints.CreateQuestionCollection);
    }

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
}

