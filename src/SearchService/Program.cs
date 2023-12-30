using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Data;
using SearchService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



var app = builder.Build();

// Initialize the database
try
{
    DbInitializer.InitDb(app).Wait();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


// Configure the HTTP request pipeline. Here we add the middleware to handle the authentication.


app.MapControllers();


app.Run();

