using Microsoft.OpenApi.Models;
using NetCrud.Application.Interfaces;
using NetCrud.Application.Services;
using NetCrud.Infrastructure.Extensions;

// Load .env file from the solution root
DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", ".env"));

var builder = WebApplication.CreateBuilder(args);

// Override connection string with environment variables
var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost"};" +
                      $"Database={Environment.GetEnvironmentVariable("DB_DATABASE") ?? "NetCrudDB"};" +
                      $"Uid={Environment.GetEnvironmentVariable("DB_USERNAME") ?? "root"};" +
                      $"Pwd={Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "YOUR_PASSWORD_HERE"};" +
                      $"Port={Environment.GetEnvironmentVariable("DB_PORT") ?? "3306"};";
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NetCrud API",
        Version = "v1",
        Description = "A .NET CRUD API with MySQL database"
    });
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add Infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

// Add Application services
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCrud API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
