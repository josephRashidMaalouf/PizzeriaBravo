using FoodStuffService.Domain.Interfaces;
using FoodStuffService.Infrastructure.Repositories;
using FoodStuffService.Domain.Converters;
using FoodStuffService.EndpointExtensions;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

var dbName = builder.Configuration.GetSection("Database")["Name"] ?? "";
var collectionName = builder.Configuration.GetSection("Database")["CollectionName"] ?? "";
var connectionString = builder.Configuration.GetConnectionString("DockerConnection") ?? "";

#if DEBUG
connectionString = builder.Configuration.GetConnectionString("LocalConnection") ?? "";
#endif

builder.Services.AddScoped<IFoodStuffRepository, FoodStuffRepository>(provider => 
    new FoodStuffRepository(dbName, collectionName, connectionString));

builder.Services.AddScoped<IFoodStuffService, FoodStuffService.Application.Services.FoodStuffService>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new GuidConverter());
});



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapEndPoints();

app.Run();

