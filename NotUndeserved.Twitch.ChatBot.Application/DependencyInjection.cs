using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using NotUndeserved.Twitch.ChatBot.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application
{
    public static class DependencyInjection {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) {
            //Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //ChatSettings
            var _twitchChatSettings = configuration.GetSection(typeof(TwitchChatSettings).Name);
            services.Configure<TwitchChatSettings>(_twitchChatSettings);

            return services;
        }
    }
}
