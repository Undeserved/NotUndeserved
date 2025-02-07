using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands
{
    public class Onlyfans : BaseChatCommand
    {
        public Onlyfans()
        {
            CommandRegexPattern = "^onlyfans( |$)";
        }
        public override async Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs)
        {
            return "!quote 502";
        }
    }
}
