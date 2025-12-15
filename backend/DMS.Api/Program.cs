using DMS.Infrastructure.DI;
using DMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Add Infrastructure services (DbContexts + Repositories)
// ---------------------------
var connectionString = builder.Configuration.GetConnectionString("DmsDatabase") 
    ?? throw new InvalidOperationException("Connection string 'DmsDatabase' not found.");
builder.Services.AddInfrastructureServices(connectionString);

// ---------------------------
// Add Controllers
// ---------------------------
builder.Services.AddControllers();

// ---------------------------
// Add Swagger
// ---------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---------------------------
// Auto migrate all registered DbContexts dynamically
// ---------------------------
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    // List of all modular DbContexts
    var dbContexts = new DbContext[]
    {
        serviceProvider.GetRequiredService<PatientDbContext>(),
        serviceProvider.GetRequiredService<DocumentDbContext>(),
        serviceProvider.GetRequiredService<UserDbContext>()
        // Add more DbContexts here if needed
    };

    foreach (var db in dbContexts)
    {
        db.Database.Migrate(); // Applies pending migrations
    }
}


// ---------------------------
// HTTP pipeline
// ---------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DMS API v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root "/"
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
