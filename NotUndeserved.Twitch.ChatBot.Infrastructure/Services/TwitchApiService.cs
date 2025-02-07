using NotUndeserved.Twitch.ChatBot.Infrastructure.Resources;
using TwitchLib.Api.Helix.Models.Clips.GetClips;
using TwitchLib.Api.Helix.Models.Games;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using TwitchLib.Api.Interfaces;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using TwitchLib.Api;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services
{
    public class TwitchApiService : ITwitchApiService {
        private readonly ITwitchAPI _twitchApi;
        private readonly ITwitchApiSettingsService _settings;
        private readonly string _broadcasterId;

        public TwitchApiService(ITwitchApiSettingsService settingsDto) {
            _settings = settingsDto;
            _settings.LoadConfig();
            _twitchApi = new TwitchAPI();
            _twitchApi.Settings.ClientId = _settings.ClientId;
            RefreshToken();
            _twitchApi.Settings.AccessToken = _settings.OAuthToken;

            var userResponse = _twitchApi.Helix.Users.GetUsersAsync(logins: new List<string> { _settings.TargetChannel }).Result;
            _broadcasterId = userResponse.Users[0].Id;
        }

        public async Task<ClipDto> GetClip(string Game, DateTime? From = null, DateTime? To = null) {
            RefreshToken();
            string clipUrl = string.Empty;
            Game? gameInfo = await GetGameInfo(Game.Trim());

            if(gameInfo != null) {
                GetClipsResponse response = await _twitchApi.Helix.Clips.GetClipsAsync(broadcasterId: _broadcasterId,
                        first: 100);
                var matchingClips = response.Clips.Where(x => x.GameId == gameInfo.Id);

                clipUrl = matchingClips.FirstOrDefault()?.Url ?? ErrorMessageRes.NoMatchingRecordsFound;
            }

            return new ClipDto {
                Broadcaster = _settings.TargetChannel,
                Game = gameInfo?.Name,
                ClipUrl = clipUrl
            };
        }

        public async Task<ClipDto> GetRandomClip(string Broadcaster) {
            RefreshToken();
            string clipUrl = string.Empty;
            var userResponse = await _twitchApi.Helix.Users.GetUsersAsync(logins: new List<string> { Broadcaster });
            if (userResponse.Users.Length == 0) {
                return new ClipDto {
                    Broadcaster = Broadcaster.ToLower(),
                    Game = string.Empty,
                    ClipUrl = clipUrl
                };
            }

            GetClipsResponse response = await _twitchApi.Helix.Clips.GetClipsAsync(broadcasterId: userResponse.Users[0].Id,
                first: 100);
            if (response.Clips.Length > 0) {
                Random random = new Random();
                int clipId = random.Next(0, response.Clips.Length);
                clipUrl = response.Clips[clipId].Url;
            }

            return new ClipDto {
                Broadcaster = Broadcaster.ToLower(),
                Game = string.Empty,
                ClipUrl = clipUrl
            };
        }

        private async Task<Game> GetGameInfo(string GameName) {
            if (string.IsNullOrWhiteSpace(GameName)) {
                return null;
            }
            GetGamesResponse response = await _twitchApi.Helix.Games.GetGamesAsync(gameNames: new List<string> { GameName });
            if(response.Games.Length > 0) {
                return response.Games[0];
            }
            return null;
        }

        private void RefreshToken() {
            if (_settings.TokenValidUntil.AddMinutes(-10) <= DateTime.Now) {
                var accessTokenResp = _twitchApi.Auth.RefreshAuthTokenAsync(_settings.RefreshToken, _settings.ClientSecret).Result;
                _settings.RefreshToken = accessTokenResp.RefreshToken;
                _settings.OAuthToken = accessTokenResp.AccessToken;
                _settings.TokenValidUntil = DateTime.Now.AddSeconds(accessTokenResp.ExpiresIn);
                _settings.PushChanges();
            }
        }
    }
}
