using MediatR;
using Microsoft.Extensions.Options;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Common.Extensions;
using NotUndeserved.Twitch.ChatBot.Application.Common.Resources;
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
    public class QuoteContains : BaseChatCommand {
        private readonly IMediator _mediator;
        private readonly TwitchChatSettings _settings;

        public QuoteContains(IMediator mediator, IOptions<TwitchChatSettings> settings) {
            CommandRegexPattern = "^quotecontains( |$)";
            _mediator = mediator;
            _settings = settings.Value;
        }

        public override async Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs) {
            if (string.IsNullOrWhiteSpace(chatCommandReceivedArgs.Command.ArgumentsAsString)) {
                return ErrorMessageRes.EmptyQuery;
            }

            List<string> keywords = chatCommandReceivedArgs.Command.ArgumentsAsList
                .Where(x => !x.StartsWith(_settings.ParameterIdentifier))
                .Select(x => x.ToLower())
                .ToList();
            List<string> parameters = chatCommandReceivedArgs.Command.ArgumentsAsList
                .Where(x => x.StartsWith(_settings.ParameterIdentifier))
                .Select(x => x.ToLower())
                .ToList();

            GetQuoteQuery quoteQuery = new GetQuoteQuery {
                Contains = keywords,
                Modifiers = parameters
            };

            IEnumerable<QuoteDto> matchingRecords = await _mediator.Send(quoteQuery);
            return FormatMessage(matchingRecords, _settings.MaxMessageLength);
        }
    }
}
