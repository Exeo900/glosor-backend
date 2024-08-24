using Core.Ports;
using Adapter.Persistence.SqlServer.Repositories;
using Adapter.Persistence.SqlServer.ConnectionFactory;
using glosor_backend.API;
using Core.UseCases.QuestionUseCases;
using Core.UseCases.QuestionCollectionUseCases;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Core.UseCases.AuthenticationUseCases;
using Adapter.Authentication;
using Swashbuckle.AspNetCore.SwaggerGen;
using glosor_backend.Swagger;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

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

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = config["JwtSettings:Issuer"],
    ValidAudience = config["JwtSettings:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!))
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionCollectionRepository, QuestionCollectionRepository>();
builder.Services.AddScoped<IWordQuestionRepository, WordQuestionRepository>();
builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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

builder.Services.AddScoped<GenerateTokenUseCase>();
builder.Services.AddScoped<RefreshTokenUseCase>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    /*WriteTo.AzureApp()*/
    .CreateLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

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