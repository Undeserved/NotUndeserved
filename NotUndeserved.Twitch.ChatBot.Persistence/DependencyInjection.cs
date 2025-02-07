using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Persistence {
    public static class DependencyInjection {
        private const string DatabaseName = "QuoteCollection";
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<QuoteDatabaseContext>(x => x.UseSqlite(configuration.GetConnectionString(DatabaseName)));
            services.AddScoped<IQuoteDatabaseContext>(x => x.GetService<QuoteDatabaseContext>());
            return services;
        }
    }
}
