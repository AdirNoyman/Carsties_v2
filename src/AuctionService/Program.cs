var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline. Here we add the middleware to handle the authentication.

app.UseAuthorization();

app.MapControllers();

app.Run();
