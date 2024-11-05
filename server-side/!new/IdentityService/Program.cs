using IdentityService.Consumers;
using IdentityService.DataAccess;
using IdentityService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "token";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.MaxAge = TimeSpan.FromDays(600);

        options.Events = new CookieAuthenticationEvents()
        {
            OnRedirectToLogin = context =>
            {
                if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == StatusCodes.Status200OK)
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                else
                    context.Response.Redirect(context.RedirectUri);

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddDbContext<AccountDbContext>(options => 
{
    var dataSourceBuilder = new Npgsql.NpgsqlDataSourceBuilder(Environment.GetEnvironmentVariable("DB_CONNECTION"));
    dataSourceBuilder.EnableDynamicJson();

    options.UseNpgsql(dataSourceBuilder.Build());
}, ServiceLifetime.Singleton);

builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IEncryptService, EncryptService>();

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

        cfg.ReceiveEndpoint("IdentityQueue", e =>
        {
            e.ConfigureConsumer<CreateAccountConsumer>(context);
            e.ConfigureConsumer<UpdateAccountConsumer>(context);
            e.ConfigureConsumer<LoginAccountConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<UserIdMiddleware>();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.EnableTryItOutByDefault();
    });
    
    app.Run("http://localhost:7001");
}
else
{
    app.Run("http://0.0.0.0:7001");
}