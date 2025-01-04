using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Services;
using Remembvoc.Infrastructure.APIs.Gens;
using Remembvoc.Infrastructure.APIs.Helpers;
using Remembvoc.Infrastructure.Data;
using Remembvoc.Infrastructure.Repositories;

namespace Remembvoc.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options 
            => options.UseSqlite("Data Source=database.db"));

        services.AddTransient<IWordRepository, WordRepository>();
        services.AddTransient<IPriorityRepository, PriorityRepository>();
        
        services.AddScoped<GroqHelper>();
        
        services.AddScoped<ISentenceGenService, SentenceGenService>();
        services.AddScoped<ISentenceGen, LIamaGen>();
        
        return services;
    }
}