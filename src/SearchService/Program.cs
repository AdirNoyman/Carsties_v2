using System.Net;
using MongoDB.Driver;
using MongoDB.Entities;
using Polly;
using Polly.Extensions.Http;
using SearchService.Data;
using SearchService.Models;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient<AuctionServiceHttpClient>().AddPolicyHandler(GetPolicy());

var app = builder.Build();




// Configure the HTTP request pipeline. Here we add the middleware to handle the authentication.


app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>
{

    // Initialize the database
    try
    {
        DbInitializer.InitDb(app).Wait();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

});


app.Run();

// If the auctions service is down, handle the eception and try again every 3 seconds
static IAsyncPolicy<HttpResponseMessage> GetPolicy()
=> HttpPolicyExtensions
.HandleTransientHttpError()
.OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
.WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
