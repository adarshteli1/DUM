using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using UserManual.Application.Interfaces;
using UserManual.Infrastructure.Repositories;

namespace UserManual.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(_ =>
                new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
           
            services.AddScoped<IManualRepository, ManualRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}