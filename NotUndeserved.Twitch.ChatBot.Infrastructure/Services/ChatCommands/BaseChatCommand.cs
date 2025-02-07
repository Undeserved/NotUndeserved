using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands {
    public abstract class BaseChatCommand {
        public string CommandRegexPattern { get; protected set; }
        public abstract Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs);

        protected virtual string FormatMessage(IEnumerable<QuoteDto> matchingQuotes, int maxMessageLength) {
            if (!matchingQuotes.Any()) {
                return ErrorMessageRes.NoMatchingRecordsFound;
            }

            int quoteCount = matchingQuotes.Count();
            if (quoteCount == 1) {
                return $"!quote {matchingQuotes.First().QuoteId}";
            }

            string message = $"{quoteCount} matches found: {string.Join("\t", matchingQuotes.Select(x => x.QuoteId))}";
            if (message.Length > maxMessageLength) {
                message = $"{quoteCount} matches found,the list exceeds the maximum message length";
            }

            return message;
        }
    }
}
