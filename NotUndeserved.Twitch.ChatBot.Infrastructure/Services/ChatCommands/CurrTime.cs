using NotUndeserved.Twitch.ChatBot.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands {
    public class CurrTime : BaseChatCommand {
        public CurrTime() {
            CommandRegexPattern = "^currtime( |$)";
        }

        public override async Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs) {
            string args = chatCommandReceivedArgs.Command.ArgumentsAsString;
            DateTime currTime = DateTime.UtcNow;
            if (int.TryParse(args, out int gmtOffset)) {
                currTime = currTime.AddHours(gmtOffset);
            }
            return currTime.ToISO8601TimeString();
        }
    }
}
