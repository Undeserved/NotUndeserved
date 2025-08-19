using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Client;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api;
using NotUndeserved.Twitch.ChatBot.Infrastructure.Services;
using LinqKit;
using NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands;
using NotUndeserved.Twitch.ChatBot.Infrastructure.Services.MessageParsers;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure
{
    public static class DependencyInjection {

        public static IServiceCollection AddTwitchClient(this IServiceCollection services, IConfiguration configuration) {
            //Twitch client
            services.AddSingleton<ITwitchClient, TwitchClient>(x => {
                ITwitchApiSettingsService chatBotSettings = x.GetRequiredService<ITwitchApiSettingsService>();
                chatBotSettings.LoadConfig();
                ITwitchApiService apiService = x.GetRequiredService<ITwitchApiService>();
                ConnectionCredentials credentials = new ConnectionCredentials(chatBotSettings.TwitchAccount, chatBotSettings.OAuthToken);
                ClientOptions clientOptions = new ClientOptions {
                    MessagesAllowedInPeriod = 600,
                    ThrottlingPeriod = TimeSpan.FromSeconds(30)
                };

                WebSocketClient customClient = new WebSocketClient(clientOptions);
                TwitchClient twitchClient = new TwitchClient(customClient);
                twitchClient.Initialize(credentials, channel: chatBotSettings.TargetChannel);
                return twitchClient;
            });

            //Chat commands
            var commands = typeof(BaseChatCommand)
                .Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => x.IsClass)
                .Where(x => x.IsSubclassOf(typeof(BaseChatCommand)));

            commands.ForEach(x => services.Add(new ServiceDescriptor(typeof(BaseChatCommand), x, ServiceLifetime.Singleton)));

            //IRC services
            services.AddSingleton<QuoteScanner>();
            services.AddSingleton<MessageSanitiser>();
            services.AddSingleton<TwitchChatService>();

            return services;
        }

        public static IServiceCollection AddTwitchApiService(this IServiceCollection services, IConfiguration configuration) {
            //Api settings
            services.AddSingleton<ITwitchApiSettingsService, TwitchApiSettingsService>();

            //Api service
            services.AddSingleton<ITwitchApiService, TwitchApiService>();            

            return services;
        }
    }
}
