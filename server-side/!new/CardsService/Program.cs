using CardsService.Consumers;
using CardsService.DataAccess;
using CardsService.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.Filters;
using Shared.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddDevelopmentEnvFile("../../../dev.env");

builder.Services.AddControllers(option => option.Filters.Add(typeof(ValidateModelAttribute)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CardDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"));
}, ServiceLifetime.Singleton);

builder.Services.AddSingleton<ICardsService, CardsService.Services.CardsService>();

builder.Services.AddMassTransit(s =>
{
    var entryAssembly = Assembly.GetExecutingAssembly();

    s.AddConsumers(entryAssembly);

    s.UsingRabbitMq((context, cfg) =>
    {
        Uri host = new Uri(builder.Environment.IsDevelopment() ? "rabbitmq://localhost/"
                                                               : "rabbitmq://host.docker.internal/");

        cfg.Host(host, h =>
        {
            h.Username(Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER"));
            h.Password(Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS"));
        });

        cfg.AutoDelete = false;
        cfg.PrefetchCount = 20;
        cfg.UseMessageRetry(r => r.Interval(2, 100));

        cfg.ReceiveEndpoint("CardsQueue", e =>
        {
            e.ConfigureConsumer<GetCardsFromIdConsumer>(context);
            e.ConfigureConsumer<GetIdsFromCardConsumer>(context);
        });
        
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<UserIdMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.EnableTryItOutByDefault();
    });

    app.Run("http://localhost:7002");
}
else
{
    app.Run("http://0.0.0.0:7002");
}