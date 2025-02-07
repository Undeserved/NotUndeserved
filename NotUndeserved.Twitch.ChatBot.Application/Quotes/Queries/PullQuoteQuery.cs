using MediatR;
using Microsoft.EntityFrameworkCore;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Common.Extensions;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Quotes.Queries {
    public class PullQuoteQuery : IRequest<QuoteDto> {
        public int QuoteId { get; set; }
    }

    public class PullQuoteHandler : IRequestHandler<PullQuoteQuery, QuoteDto> {
        private readonly IQuoteDatabaseContext _context;
        private readonly Random rng;

        public PullQuoteHandler(IQuoteDatabaseContext context) {
            _context = context;
            rng = new Random();
        }

        public async Task<QuoteDto> Handle(PullQuoteQuery request, CancellationToken cancellationToken) {
            if (request.QuoteId == 0) {
                int quoteCount = _context.Quotes.Count();
                request.QuoteId = rng.RollDice(quoteCount);
            }

            return await _context.Quotes
                .Where(x => x.Id == request.QuoteId)
                .Select(x => new QuoteDto {
                    QuoteId = x.Id,
                    Date = x.Date,
                    Game = x.Game,
                    QuoteContent = x.Content
                })
                .FirstOrDefaultAsync(cancellationToken) ?? new QuoteDto { QuoteId = request.QuoteId };
        }
    }
}
