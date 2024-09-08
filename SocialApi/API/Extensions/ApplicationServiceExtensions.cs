using API.Data;
using API.Interface;
using API.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicaitonServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MYSQL_CONNECTION_STRING");

            #region Controllers
            services.AddControllers();
            #endregion

            #region Scoped Services
            services.AddScoped<ITokenService, TokenService>();
            #endregion

            #region Database Context
            services.AddDbContext<DataContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            #endregion

            #region API Versioning
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            #endregion

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev",
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:4200", "https://localhost:4200")
                        .AllowCredentials());
            });
            #endregion

            #region HTTP Logging
            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });
            #endregion

            return services;
        }
    }
}
