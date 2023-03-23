using GitRepositoryTracker;
using GitRepositoryTracker.DButil;
using GitRepositoryTracker.Interfaces;
using GitRepositoryTracker.Repositories;
using GitRepositoryTracker.Services;
using Microsoft.EntityFrameworkCore;
using Octokit;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddDbContext<GitRepoContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
//builder.Services.AddLogging(configure =>
//{
//    configure.AddConsole();
//});

builder.Services.AddScoped<IUIGenericRepository, UIRepository>();
builder.Services.AddScoped<IGitHubAPIService, GitHubAPIService>();
builder.Services.AddScoped<IRepositoryDeserializer, RepositoryDeserializer>();
builder.Services.AddScoped<IGitAPIRepository, GitAPIRepository>();

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

app.Run();
