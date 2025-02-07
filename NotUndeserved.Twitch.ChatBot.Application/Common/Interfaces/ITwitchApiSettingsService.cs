using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces
{
    public interface ITwitchApiSettingsService
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RefreshToken { get; set; }
        public string TwitchAccount { get; set; }
        public string TargetChannel { get; set; }
        public string QuoteAuthority { get; set; }
        public string? OAuthToken { get; set; }
        public DateTime TokenValidUntil { get; set; }

        public void LoadConfig();
        public void PushChanges();
    }
}
