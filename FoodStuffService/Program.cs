using FoodStuffService.Application.Services;
using FoodStuffService.Domain.Interfaces;
using FoodStuffService.Infrastructure.Repositories;
using FoodStuffService.Domain.Converters;
using FoodStuffService.EndpointExtensions;
using Microsoft.AspNetCore.Http.Json;
using RabbitMQ.Client;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var dbName = builder.Configuration.GetSection("Database")["Name"] ?? "";
var collectionName = builder.Configuration.GetSection("Database")["CollectionName"] ?? "";
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddScoped<IFoodStuffRepository, FoodStuffRepository>(provider =>
    new FoodStuffRepository(dbName, collectionName, connectionString));

builder.Services.AddScoped<IFoodStuffService, FoodStuffService.Application.Services.FoodStuffService>();
builder.Services.AddSingleton<IMessageService, RabbitMqService>();


builder.Services.Configure<JsonOptions>(options => { options.SerializerOptions.Converters.Add(new GuidConverter()); });

builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>(_ => new()
{
    Uri = new Uri(builder.Configuration["RabbitMQ:MqUri"] ?? string.Empty),
    ClientProvidedName = builder.Configuration["RabbitMQ:ClientProvidedName"] ?? string.Empty,
    HostName = builder.Configuration["RabbitMQ:HostName"] ?? string.Empty,
    UserName = builder.Configuration["RabbitMQ:UserName"] ?? string.Empty,
    Password = builder.Configuration["RabbitMQ:Password"] ?? string.Empty,
});



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();


app.MapOpenApi();
app.MapScalarApiReference(options => { options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient); });


app.UseHttpsRedirection();

app.MapEndPoints();

app.Run();