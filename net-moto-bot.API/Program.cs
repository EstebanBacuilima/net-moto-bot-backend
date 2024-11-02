using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using net_moto_bot.API.Extensions;
using net_moto_bot.Application.Interfaces.Custom;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Application.Services.Custom;
using net_moto_bot.Application.Services.Public;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connectoins;
using net_moto_bot.Infrastructure.Repositories.Public;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the PostgreSQL context.
builder.Services.AddDbContext<PostgreSQLContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"));
});

#region Declare all repositories sorted alphabetically
// Public schema
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Services sorted alphabetically.
// Singleton
builder.Services.AddSingleton<IJWTService, JWTService>();

// Public schema
builder.Services.AddScoped<IUserService, UserService>();
#endregion

// Enable Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", policyBuilder =>
    {
        // Read cors from json file configuration.
        List<string> cors = [];
        builder.Configuration.GetSection("Cors").Bind(cors);
        cors.ForEach(cor => policyBuilder.WithOrigins(cor).Build());
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.SetIsOriginAllowed(_ => true);
    });
});

// Enable JWT.
builder.Services.AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? "")),
        };
        //options.Events = new AuthorizeHandler();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("EnableCORS");

app.UseAuthorization();

app.MapControllers();

app.ConfigureExceptionMiddleware();

app.Run();
