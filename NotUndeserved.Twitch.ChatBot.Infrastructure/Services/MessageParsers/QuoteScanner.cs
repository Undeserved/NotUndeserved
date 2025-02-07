using MediatR;
using Microsoft.Extensions.Options;
using NotUndeserved.Twitch.ChatBot.Application.Quotes.Commands;
using NotUndeserved.Twitch.ChatBot.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.MessageParsers {
    public class QuoteScanner {
        public const string MessageRegexFormat = "^Quote #(?<QuoteId>[0-9]+) (?<Quote>.*?) \\[(?<Game>[^\\]]*)] \\[(?<QuoteDate>([0-2][0-9]|(3)[0-1])(\\/)(((0)[0-9])|((1)[0-2]))(\\/)\\d{4})\\]";
        private readonly IMediator _mediator;
        private readonly Regex _quoteRegex;

        public QuoteScanner(IMediator mediator) {
            _mediator = mediator;
            _quoteRegex = new Regex(MessageRegexFormat);
        }

        public async Task ParseMessage(OnMessageReceivedArgs messageReceivedArgs) {
            Match matches = _quoteRegex.Match(messageReceivedArgs.ChatMessage.Message);
            if (matches.Success) {
                UpsertQuoteCommand quote = new UpsertQuoteCommand {
                    QuoteId = int.Parse(matches.Groups[nameof(quote.QuoteId)].Value),
                    Quote = matches.Groups.GetValueOrDefault(nameof(quote.Quote))?.Value,
                    Game = matches.Groups.GetValueOrDefault(nameof(quote.Game))?.Value,
                    QuoteDate = matches.Groups[nameof(quote.QuoteDate)].Value.Rfc5322ToDate()
                };
                await _mediator.Send(quote);
            }
        }
    }
}
