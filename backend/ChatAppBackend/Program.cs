using System.Security.Claims;
using System.Text;
using ChatAppBackend;
using ChatAppBackend.Auth;
using ChatAppBackend.Entities;
using ChatAppBackend.Facades;
using ChatAppBackend.Hubs;
using ChatAppBackend.Services;
using ChatAppBackend.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
AppSettings settings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>()!;

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApiDocument(document => document.DocumentName = "v1");

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserFacade>();
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SigningKey))
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
