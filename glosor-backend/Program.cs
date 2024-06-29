using Core.Ports;
using Adapter.Persistence.SqlServer.Repositories;
using Adapter.Persistence.SqlServer.ConnectionFactory;
using glosor_backend.API;
using Core.UseCases.QuestionUseCases;
using Core.UseCases.QuestionCollectionUseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins(
                    "https://localhost:*",
                    "https://127.0.0.1:*"
                )
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionCollectionRepository, QuestionCollectionRepository>();
builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>();

builder.Services.AddScoped<CreateQuestionUseCase>();
builder.Services.AddScoped<GetAllQuestionsUseCase>();
builder.Services.AddScoped<GetQuestionUseCase>();
builder.Services.AddScoped<UpdateQuestionUseCase>();
builder.Services.AddScoped<DeleteQuestionUseCase>();

builder.Services.AddScoped<GetAllQuestionsCollectionUseCase>();
builder.Services.AddScoped<GetQuestionsCollectionUseCase>();
builder.Services.AddScoped<CreateQuestionsCollectionUseCase>();
builder.Services.AddScoped<UpdateQuestionsCollectionUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureApi();

app.UseCors("AllowFrontend");

app.Run();