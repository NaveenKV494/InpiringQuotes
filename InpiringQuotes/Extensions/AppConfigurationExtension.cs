using InspiringQuotes.Data.DBContext;
using InspiringQuotes.Data.Repositories.Abstractions;
using InspiringQuotes.Data.Repositories.Implementations;
using InspiringQuotes.Service.Abstractions;
using InspiringQuotes.Service.Implementations;
using InspiringQuotes.Service.Infrastructure.AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace InpiringQuotes.Extensions
{
    public static class AppConfigurationExtension
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<QuoteDbContext>(options => options.UseSqlite(configuration.GetConnectionString("defaultConnection")));
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            services.AddScoped<IQuoteService, QuoteService>();

            return services;
        }
    }
}
