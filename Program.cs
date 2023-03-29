using GitRepositoryTracker;
using GitRepositoryTracker.DButil;
using GitRepositoryTracker.Interfaces;
using GitRepositoryTracker.Repositories;
using GitRepositoryTracker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Octokit;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Add API explorer and Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GitHubRepositoryTracker", Version = "v1" });

    // Add JWT authentication security scheme.
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
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
            Array.Empty<string>()
        }
    });
});

// Add hosted services and AutoMapper.
builder.Services.AddHostedService<GitHubDataFetcher>();
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Configure database connection.
builder.Services.AddDbContext<GitRepoContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), options =>
    {
        options.EnableRetryOnFailure();
        options.CommandTimeout(300);
    });
});

// Configure GitHub client.
builder.Services.AddScoped<IGitHubClient>(provider =>
{
    var appSettings = provider.GetRequiredService<IConfiguration>();
    var githubSettings = builder.Configuration.GetSection("GitHubSettings");

    var githubClient = new GitHubClient(new ProductHeaderValue("GitRepoTracker"))
    {
        Credentials = new Credentials(githubSettings["GitHubAccessToken"])
    };

    return githubClient;
});

// Register repositories and services.
builder.Services.AddScoped<IUIGenericRepository, UIRepository>();
builder.Services.AddScoped<IGitHubAPIService, GitHubAPIService>();
builder.Services.AddScoped<IRepositoryDeserializer, RepositoryDeserializer>();
builder.Services.AddScoped<IGitAPIRepository, GitAPIRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Configure caching and Identity.
builder.Services.AddScoped<IMemoryCache, MemoryCache>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

})
    .AddEntityFrameworkStores<GitRepoContext>()
    .AddDefaultTokenProviders();

// Configure JWT authentication.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure Swagger and Swagger UI.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GitHubRepositoryTracker v1");
});

// Enable HTTPS redirection.
app.UseHttpsRedirection();

// Configure authentication and authorization.
app.UseAuthentication();
app.UseAuthorization();

// Map controllers and run the application.
app.MapControllers();
app.Run();

