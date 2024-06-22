using Core.Ports;
using glosor_backend;
using Adapter.Persistence.SqlServer.Repositories;
using Core.UseCases;

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
builder.Services.AddScoped<CreateQuestionUseCase>();
builder.Services.AddScoped<GetAllQuestionsUseCase>();
builder.Services.AddScoped<GetQuestionUseCase>();
builder.Services.AddScoped<UpdateQuestionUseCase>();
builder.Services.AddScoped<DeleteQuestionUseCase>();

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