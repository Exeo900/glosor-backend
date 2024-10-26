using Core.Entities.Enums;
using Core.Entities;
using glosor_backend.Dtos;
using Core.UseCases.QuestionUseCases;
using Core.Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace glosor_backend.API;

public static class QuestionsEndpoints 
{
    [Authorize]
    public static async Task<IResult> CreateQuestionRequest(CreateQuestionUseCase createQuestionUseCase, CreateQuestionRequest createQuestionRequest)
    {
        if (createQuestionRequest == null)
        {
            return Results.BadRequest();
        }

        if (!Enum.IsDefined(typeof(QuestionType), createQuestionRequest.QuestionTypeId))
        {
            return Results.BadRequest($"Question type id '{createQuestionRequest.QuestionTypeId}' does not exists");
        }

        try
        {
            await createQuestionUseCase.Execute(new Core.ValueObjects.QuestionObjects.CreateQuestionRequest()
            {
                Text = createQuestionRequest.Text,
                AnswerText = createQuestionRequest.AnswerText,
                QuestionTypeId = createQuestionRequest.QuestionTypeId,
                Description = createQuestionRequest.Description,
                QuestionCollectionId = createQuestionRequest.QuestionCollectionId
            });
        }
        catch (DuplicateQuestionException dqe)
        {
            return Results.Conflict(dqe.Message);
        }

        return Results.Ok("Created");
    }

    [Authorize]
    public static async Task<IEnumerable<Question>> GetAllQuestionRequests(GetAllQuestionsUseCase getAllQuestionsUseCase)
    {
        return await getAllQuestionsUseCase.Execute();
    }


    [Authorize]
    public static async Task<IResult> GetQuestionsBySearchTermRequests(GetQuestionBySearchTermUseCase getQuestionBySearchTermUseCase, string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm) || searchTerm.Count() <= 1)
        {
            return Results.Problem("The search term is too short.");
        }

        var existingQuestions = await getQuestionBySearchTermUseCase.Execute(searchTerm);

        return Results.Ok(existingQuestions);
    }

    [Authorize]
    public static async Task<Question?> GetQuestionRequest(GetQuestionUseCase getAllQuestionsUseCase, Guid id)
    {
        return await getAllQuestionsUseCase.Execute(id);
    }

    [Authorize]
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

    [Authorize]
    public static async Task<bool> DeleteQuestionRequest(DeleteQuestionUseCase deleteQuestionUseCase, Guid id)
    {
        return await deleteQuestionUseCase.Execute(id);
    }

    [Authorize]
    public static async Task<IResult?> GetRandomQuestion(GetRandomQuestionUseCase getRandomQuestionUseCase, Guid questionCollectionId)
    {
        try
        {
            var result = await getRandomQuestionUseCase.Execute(questionCollectionId);

            return Results.Ok(result);
        }
        catch (InsufficientAlternativesException ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [Authorize]
    public static async Task<bool?> ValidateGuess(ValidateQuestionGuessUseCase validateQuestionGuessUseCase, Guid questionId, string userGuess)
    {
        return await validateQuestionGuessUseCase.Execute(questionId, userGuess);
    }
}
