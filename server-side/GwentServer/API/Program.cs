using Application.Services;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using Game.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddDbContext<AccountDbContext>();

builder.Services.AddScoped<AccountsService>();
builder.Services.AddScoped<RoomsService>();
builder.Services.AddScoped<AccountsRepository>();

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
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<GameHub>("/hub/game");

app.Run();
