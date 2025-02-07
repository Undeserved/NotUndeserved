using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Models;
using NotUndeserved.Twitch.ChatBot.Application.Quotes.Queries;
using NotUndeserved.Twitch.ChatBot.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands {
    public class QuoteQuery : BaseChatCommand {
        private readonly IMediator _mediator;
        private readonly TwitchChatSettings _settings;

        public QuoteQuery(IMediator mediator, IOptions<TwitchChatSettings> settings) {
            CommandRegexPattern = "^query( |$)";
            _mediator = mediator;
            _settings = settings.Value;   
        }

        public override async Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs) {
            GetQuoteQuery query;
            try {
                query = JsonConvert.DeserializeObject<GetQuoteQuery>(chatCommandReceivedArgs.Command.ArgumentsAsString);
                if (query.AreArgsEmpty()) {
                    return ErrorMessageRes.EmptyQuery;
                }
            } catch (Exception) {
                return ErrorMessageRes.FormatError;
            }

            IEnumerable<QuoteDto> matchingQuotes = await _mediator.Send(query);
            return FormatMessage(matchingQuotes, _settings.MaxMessageLength);
        }
    }
}
