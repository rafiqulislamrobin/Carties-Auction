using AuctionService;
using AuctionService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(x =>
{
    //outbox system for rabbitmq bus system 
    x.AddEntityFrameworkOutbox<AuctionDbContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(10);

        o.UsePostgres();
        o.UseBusOutbox();
    });

    x.UsingRabbitMq((context, cfg) =>
    cfg.ConfigureEndpoints(context));

    x.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(connectionString);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    DbInitialSeeder.DbInitDb(app);
}
catch (Exception e)
{

	Console.WriteLine(e.Message);
}
app.Run();
