using MediatR;
using Microsoft.EntityFrameworkCore;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using NotUndeserved.Twitch.ChatBot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Settings.Commands {
    public class UpdateSettingsCommand : IRequest {
        public string? ClientId {  get; set; }
        public string? ClientSecret { get; set; }
        public string? RefreshToken { get; set; }
        public string? TwitchAccount { get; set; }
        public string? TargetChannel { get; set; }
        public string? QuoteAuthority {  get; set; }
        public string? OAuthToken { get; set; }
        public DateTime TokenValidUntil {  get; set; }
    }

    public class UpdateSettingsHandler : IRequestHandler<UpdateSettingsCommand> {
        private readonly IQuoteDatabaseContext _dbContext;

        public UpdateSettingsHandler(IQuoteDatabaseContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateSettingsCommand request, CancellationToken cancellationToken) {
            TwitchApiSettings? settings = await _dbContext
                .Settings
                .FirstOrDefaultAsync(cancellationToken);

            if (settings == null) {
                settings = new TwitchApiSettings();
                _dbContext.Settings.Add(settings);
            }

            if(!string.IsNullOrWhiteSpace(request.ClientId)) {
                settings.ClientId = request.ClientId;
            }

            if(!string.IsNullOrWhiteSpace(request.ClientSecret)) {
                settings.ClientSecret = request.ClientSecret;
            }

            if(!string.IsNullOrWhiteSpace(request.RefreshToken)) {
                settings.RefreshToken = request.RefreshToken;
            }

            if(!string.IsNullOrWhiteSpace(request.TwitchAccount)) {
                settings.TwitchAccount = request.TwitchAccount;
            }

            if (!string.IsNullOrWhiteSpace(request.TargetChannel)) {
                settings.TargetChannel = request.TargetChannel;
            }

            if (!string.IsNullOrWhiteSpace(request.QuoteAuthority)) {
                settings.QuoteAuthority = request.QuoteAuthority;
            }

            if (!string.IsNullOrWhiteSpace(request.OAuthToken)) {
                settings.OAuthToken = request.OAuthToken;
            }

            if (request.TokenValidUntil >= DateTime.MinValue) {
                settings.TokenValidUntil = request.TokenValidUntil;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
