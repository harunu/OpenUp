using iPractice.Api;
using iPractice.Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using iPractice.Api.UseCases.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger configuration
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "iPractice APIs"
    });
});

// Add CORS policy to allow all origins 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterClientCommand).Assembly);
});

// Add DbContext with SQLite configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

// Register repositories for dependency injection
builder.Services.AddScoped<IPsychologistSqlRepository, PsychologistSqlRepository>();
builder.Services.AddScoped<IClientSqlRepository, ClientSqlRepository>();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Enable CORS globally
app.UseCors("AllowAll");

// Swagger setup for development environment
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iPractice APIs v1"));
}

// Set up request routing
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Initialize database with seed data
await InitializeDatabase(app);

app.Run();

static async Task InitializeDatabase(IHost app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    await context.Database.MigrateAsync();
    var seedData = new SeedData(context);
    seedData.Seed();
}

