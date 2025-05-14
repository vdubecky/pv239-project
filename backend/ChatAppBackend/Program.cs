using System.Security.Claims;
using System.Text;
using ChatAppBackend;
using ChatAppBackend.Auth;
using ChatAppBackend.Entities;
using ChatAppBackend.Facades;
using ChatAppBackend.Hubs;
using ChatAppBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApiDocument(document => document.DocumentName = "v1");

builder.Services.AddScoped<ChatAppHub>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserFacade>();

builder.Services.AddScoped<ConversationService>();
builder.Services.AddScoped<ConversationFacade>();

builder.Services.AddScoped<MessageService>();

builder.Services.AddTransient<AuthTokenHandler>();
builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

builder.Services.AddDbContext<ChatAppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "your_issuer",
        ValidAudience = "your_audience",
        IssuerSigningKey = new SymmetricSecurityKey("M5\\,3c>\u00a3vAz<XIVhfihJYY![*&r4xKL4b\"4Y>4)F"u8.ToArray())
    };
});
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RegisteredUser", policy => policy.RequireClaim(ClaimTypes.NameIdentifier));

// TODO: Move to a better place
Directory.CreateDirectory("profile-pictures");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHub<ChatAppHub>("/chatAppHub");

app.Run();
