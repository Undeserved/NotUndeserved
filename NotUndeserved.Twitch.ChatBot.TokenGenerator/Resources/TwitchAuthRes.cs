using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.TokenGenerator.Resources {
    public static class TwitchAuthRes {
        public static string RedirectUri { get; } = "http://localhost:8080/redirect/";
        public static string ClientId { get; } = "<ClientId>";
        public static string ClientSecret { get; } = "<ClientSecret>";
    }
}
