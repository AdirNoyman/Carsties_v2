using AuctionService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDBContext>(options =>

    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }

    );
// Register the Mappings profile to the application's memory
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<AuctionDBContext>(o =>
    {
        // If the Auction created and Rabbit is avaialble, send the event to Rabbit immditalty, but if not, retry sending every 10 seconds
        o.QueryDelay = TimeSpan.FromSeconds(10);
        o.UsePostgres();
        o.UseBusOutbox();
    }
    );

    // Connect to the RabbitMQ service through localhost
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline. Here we add the middleware to handle the authentication.

app.UseAuthorization();

app.MapControllers();

try
{
    DbInitilaizer.InitDb(app);
}
catch (Exception ex)
{
    Console.WriteLine("Error while seeding the DB 🫣: " + ex.Message);
}

app.Run();
