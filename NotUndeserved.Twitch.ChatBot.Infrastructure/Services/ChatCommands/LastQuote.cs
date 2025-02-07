using MediatR;
using Microsoft.Extensions.Options;
using NotUndeserved.Twitch.ChatBot.Application.Models;
using NotUndeserved.Twitch.ChatBot.Application.Quotes.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands {
    public class LastQuote : BaseChatCommand {
        private readonly IMediator _mediator;
        public LastQuote(IMediator mediator) {
            CommandRegexPattern = "^lastquote( |$)";
            _mediator = mediator;
        }

        public override async Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs) {
            GetLastQuoteQuery lastQuoteQuery = new GetLastQuoteQuery();
            int lastQuoteId = await _mediator.Send(lastQuoteQuery);
            return $"!quote {lastQuoteId}";
        }
    }

    public class GetLastQuote : BaseChatCommand {
        private readonly IMediator _mediator;
        public GetLastQuote(IMediator mediator) {
            CommandRegexPattern = "^getlastquote( |$)";
            _mediator = mediator;
        }

        public override async Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs) {
            GetLastQuoteQuery lastQuoteQuery = new GetLastQuoteQuery();
            int lastQuoteId = await _mediator.Send(lastQuoteQuery);
            return $"!quote {lastQuoteId}";
        }
    }

    public class LatestQuote : BaseChatCommand {
        private readonly IMediator _mediator;
        public LatestQuote(IMediator mediator) {
            CommandRegexPattern = "^latestquote( |$)";
            _mediator = mediator;
        }

        public override async Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs) {
            GetLastQuoteQuery lastQuoteQuery = new GetLastQuoteQuery();
            int lastQuoteId = await _mediator.Send(lastQuoteQuery);
            return $"!quote {lastQuoteId}";
        }
    }
}
