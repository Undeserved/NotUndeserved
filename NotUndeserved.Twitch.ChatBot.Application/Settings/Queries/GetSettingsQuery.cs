using MediatR;
using Microsoft.EntityFrameworkCore;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Settings.Queries
{
    public class GetSettingsQuery : IRequest<TwitchApiSettingsDto> {
    }
    public class GetSettingsHandler : IRequestHandler<GetSettingsQuery, TwitchApiSettingsDto> {
        private readonly IQuoteDatabaseContext _dbContext;

        public GetSettingsHandler(IQuoteDatabaseContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<TwitchApiSettingsDto> Handle(GetSettingsQuery request, CancellationToken cancellationToken) {
            return await _dbContext.Settings
                .Select(x => new TwitchApiSettingsDto {
                    ClientId = x.ClientId,
                    ClientSecret = x.ClientSecret,
                    QuoteAuthority = x.QuoteAuthority,
                    RefreshToken = x.RefreshToken,
                    TargetChannel = x.TargetChannel,
                    TwitchAccount = x.TwitchAccount,
                    OAuthToken = x.OAuthToken,
                    TokenValidUntil = x.TokenValidUntil
                })
                .FirstOrDefaultAsync(cancellationToken) ?? new TwitchApiSettingsDto();
        }
    }
}
