using System.Net;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<AuctionServiceHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddMassTransit(x =>
{
    // Tell MassTransit about our Searchservice consumer
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    // Set the endpoint name to kebab case in the Masstransit endpoint so it will be easier to read in the RabbitMQ management console
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false)); //

    // Connect to the RabbitMQ service through localhost
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

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
