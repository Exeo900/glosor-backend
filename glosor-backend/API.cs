using Core.UseCases;
using glosor_backend.Dtos;

namespace glosor_backend;

public static class API
{
    public static void ConfigureApi(this WebApplication webApplicationBuilder)
    {
        webApplicationBuilder.MapGet("siteInfo", SiteInfo);
        webApplicationBuilder.MapPost("glosorQuestion", CreateQuestionRequest);
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

       await createQuestionUseCase.Execute(new Core.ValueObjects.CreateQuestionRequest()
       {
            Text = createQuestionRequest.Text,
            AnswerText = createQuestionRequest.AnswerText,
            QuestionType = createQuestionRequest.QuestionType
        });

        throw new NotImplementedException();
    }
}

