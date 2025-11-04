using System.Text;
using DreamSoftData.Config;
using DreamSoftData.Context;
using DreamSoftLogic.Config;
using DreamSoftLogic.Services.Email;
using DreamSoftModel.Config;
using DreamSoftModel.Models.SecurityConfig;
using DreamSoftWebApi.Middleware;
using DreamSoftWebApi.Permissions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Resend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// add database context and repositories
builder.Services.AddDbContext<DreamSoftDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DreamSoftContext"));
    options.LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Register the DreamSoft jwt settings configuration
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
builder.Services.AddOptions();
builder.Services.AddHttpClient<ResendClient>();
builder.Services.Configure<ResendClientOptions>(o =>
{
    o.ApiToken = builder.Configuration["Email:ResendApiKey"] ?? string.Empty;
});
builder.Services.AddTransient<IResend, ResendClient>();
builder.Services.AddSingleton<IVerificationCodeManager, VerificationCodeManager>();

builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DreamSoft Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

// Add authentication and authorization settings
var jwtSetting = builder.Configuration.GetSection("JwtSettings").Get<JwtSetting>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(jwtSetting!.Secret);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSetting.Issuer,
            ValidAudience = jwtSetting.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("JWT authentication failed: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddFluentValidationAutoValidation();

// setup repositories service
builder.Services.AddDreamSoftData();
//setup logic services
builder.Services.AddDreamSoftLogicServices();
//setup model profiles
builder.Services.AddModelProfiles();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        b => b.WithOrigins("http://localhost:5173", "http://localhost:5279", "https://localhost:7189")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

// Only redirect to HTTPS in production
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseMiddleware<RateLimitingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<PermissionManager>();
app.UseMiddleware<ApiExceptionsMapper>();

app.MapControllers();

app.Run();