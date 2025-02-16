﻿using MediatR;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Quotes.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands {
    public class PullQuote : BaseChatCommand {
        private IMediator _mediator;

        public PullQuote(IMediator mediator) {
            CommandRegexPattern = "^pullquote( |$)";
            _mediator = mediator;
        }

        public override async Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs) {
            int.TryParse(chatCommandReceivedArgs.Command.ArgumentsAsList.FirstOrDefault(), out int quoteId);
            PullQuoteQuery query = new PullQuoteQuery { QuoteId = quoteId };
            QuoteDto response  = await _mediator.Send(query);
            return response.FormattedQuote;
        }
    }
}
