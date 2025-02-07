using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using NotUndeserved.Twitch.ChatBot.Application.Settings.Commands;
using NotUndeserved.Twitch.ChatBot.Application.Settings.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services {
    public class TwitchApiSettingsService : ITwitchApiSettingsService {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? RefreshToken { get; set; }
        public string? TwitchAccount { get; set; }
        public string? TargetChannel { get; set; }
        public string? QuoteAuthority { get; set; }
        public string? OAuthToken {  get; set; }
        public DateTime TokenValidUntil { get; set; }

        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// I hate this class like you wouldn't believe
        /// </summary>
        /// <param name="serviceProvider">Classic dependency injection slop.</param>
        public TwitchApiSettingsService(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public void LoadConfig() {
            var scope = _serviceProvider.CreateScope();
            var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            GetSettingsQuery settingsQuery = new GetSettingsQuery();
            TwitchApiSettingsDto twitchApiSettings = _mediator.Send(settingsQuery).Result;

            ClientId = twitchApiSettings.ClientId;
            ClientSecret = twitchApiSettings.ClientSecret;
            RefreshToken = twitchApiSettings.RefreshToken;
            TwitchAccount = twitchApiSettings.TwitchAccount;
            TargetChannel = twitchApiSettings.TargetChannel;
            QuoteAuthority = twitchApiSettings.QuoteAuthority;
            OAuthToken = twitchApiSettings.OAuthToken;
            TokenValidUntil = twitchApiSettings.TokenValidUntil;
        }

        public void PushChanges() {
            var scope = _serviceProvider.CreateScope();
            var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            UpdateSettingsCommand command = new UpdateSettingsCommand {
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                RefreshToken = RefreshToken,
                TwitchAccount = TwitchAccount,
                TargetChannel = TargetChannel,
                QuoteAuthority = QuoteAuthority,
                OAuthToken = OAuthToken,
                TokenValidUntil = TokenValidUntil
            };
            _mediator.Send(command);
        }
    }
}
