
using DMS.Application.Interfaces;
using DMS.Infrastructure.Persistence;
using DMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DMS.Infrastructure.DI
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            // ---------------------------
            // Register DbContexts (module-specific)
            // Each context uses a separate migration history table to avoid conflicts
            // ---------------------------
            services.AddDbContext<PatientDbContext>(options =>
                options.UseSqlServer(connectionString, b => 
                {
                    b.MigrationsAssembly("DMS.Infrastructure");
                    b.MigrationsHistoryTable("__PatientMigrationsHistory", "dbo");
                }));

            services.AddDbContext<DocumentDbContext>(options =>
                options.UseSqlServer(connectionString, b => 
                {
                    b.MigrationsAssembly("DMS.Infrastructure");
                    b.MigrationsHistoryTable("__DocumentMigrationsHistory", "dbo");
                }));

            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(connectionString, b => 
                {
                    b.MigrationsAssembly("DMS.Infrastructure");
                    b.MigrationsHistoryTable("__UserMigrationsHistory", "dbo");
                }));

            // ---------------------------
            // Register repositories
            // ---------------------------
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // ---------------------------
            // Optional: file storage, audit logging, etc.
            // ---------------------------
            // services.AddScoped<IFileStorageService, LocalFileStorageService>();

            return services;
        }
    }
}
