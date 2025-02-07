using MediatR;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using NotUndeserved.Twitch.ChatBot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Quotes.Commands {
    public class UpsertQuoteCommand : IRequest {
        public int QuoteId {  get; set; }
        public string? Quote {  get; set; }
        public string? Game {  get; set; }
        public DateTime QuoteDate { get; set; }
    }

    public class UpsertQuoteHandler : IRequestHandler<UpsertQuoteCommand> {
        private readonly IQuoteDatabaseContext _context;

        public UpsertQuoteHandler(IQuoteDatabaseContext context) {
            _context = context;
        }

        public async Task Handle(UpsertQuoteCommand request, CancellationToken cancellationToken) {
            Quote? quote = _context.Quotes
                .Where(x=>x.Id == request.QuoteId)
                .FirstOrDefault();

            if (quote == null) {
                quote = new Quote { Id = request.QuoteId };
                _context.Quotes.Add(quote);
            }

            quote.Content = request.Quote ?? string.Empty;
            quote.Game = request.Game ?? string.Empty;
            quote.Date = request.QuoteDate;

            _context.QuoteLogs.Add(new QuoteLog {
                QuoteId = request.QuoteId,
                RequestDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
