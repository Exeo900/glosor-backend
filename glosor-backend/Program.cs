using Core.Ports;
using Adapter.Persistence.SqlServer.Repositories;
using Adapter.Persistence.SqlServer.ConnectionFactory;
using glosor_backend.API;
using Core.UseCases.QuestionUseCases;
using Core.UseCases.QuestionCollectionUseCases;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins(
                    "http://localhost:*",
                    "http://localhost:5173",
                    "https://localhost:*",
                    "https://127.0.0.1:*",
                    "https://glosor-frontend.azurewebsites.net"
                )
        .AllowAnyMethod()
        .AllowAnyHeader(); 
    });
});

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionCollectionRepository, QuestionCollectionRepository>();
builder.Services.AddScoped<IWordQuestionRepository, WordQuestionRepository>();
builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>();

builder.Services.AddScoped<CreateQuestionUseCase>();
builder.Services.AddScoped<GetAllQuestionsUseCase>();
builder.Services.AddScoped<GetQuestionUseCase>();
builder.Services.AddScoped<UpdateQuestionUseCase>();
builder.Services.AddScoped<DeleteQuestionUseCase>();
builder.Services.AddScoped<GetRandomQuestionUseCase>();
builder.Services.AddScoped<ValidateQuestionGuessUseCase>();

builder.Services.AddScoped<GetAllQuestionsCollectionUseCase>();
builder.Services.AddScoped<GetQuestionsCollectionUseCase>();
builder.Services.AddScoped<CreateQuestionsCollectionUseCase>();
builder.Services.AddScoped<UpdateQuestionsCollectionUseCase>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            });
        }
    });
});

app.ConfigureApi();

app.UseCors("AllowFrontend");

app.Run();