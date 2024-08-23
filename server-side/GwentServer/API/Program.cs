using Microsoft.AspNetCore.Authentication.Cookies;
using MySqlConnector;
using Application.Services;
using DataAccess.Repositories;
using DataAccess.Interfaces;
using DataAccess.Databases;
using DataAccess.Entities;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services.AddSingleton<MySqlDbContext>();

builder.Services.AddSingleton<IRepository<CardEntity>, CardsRepository>();
builder.Services.AddSingleton<IRepository<AccountEntity>, AccountsRepository>();

builder.Services.AddSingleton<AccountsService>();
builder.Services.AddSingleton<EncryptService>();
builder.Services.AddSingleton<RoomsService>();
builder.Services.AddSingleton<DecksService>();
builder.Services.AddSingleton<DecksConvertorService>();

builder.Services.AddAuthorization();
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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://known-tough-doe.ngrok-free.app");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.Run();
}
else
{
    app.Run("http://0.0.0.0:80");
}
