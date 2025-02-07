using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Common.Extensions;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using NotUndeserved.Twitch.ChatBot.Application.Common.Resources;
using NotUndeserved.Twitch.ChatBot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Quotes.Queries {
    public class GetQuoteQuery : IRequest<IEnumerable<QuoteDto>> {
        public List<string> Contains { get; set; } = new List<string>();
        public List<string> Modifiers { get; set; } = new List<string>();
        public string? Game { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool MultipleResults { get; set; } = true; //todo
        public bool ExactTokenMatch { get; set; } = false; //todo
        public bool CaseSensitive { get; set; } = false; //todo
        public int LevenshteinDistanceThreshold { get; set; } = 0; //todo

        public bool AreArgsEmpty() {
            return (!Contains.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                && From == null
                && To == null
                && string.IsNullOrWhiteSpace(Game);
        }
    }

    public class GetQuoteHandler : IRequestHandler<GetQuoteQuery, IEnumerable<QuoteDto>> {
        private readonly IQuoteDatabaseContext _context;

        public GetQuoteHandler(IQuoteDatabaseContext context) {
            _context = context;
        }

        public async Task<IEnumerable<QuoteDto>> Handle(GetQuoteQuery request, CancellationToken cancellationToken) {
            ExpressionStarter<Quote> predicate = PredicateBuilder.New<Quote>(true);

            if (request.Contains != null) {
                if (request.Modifiers.Contains(CommandModifiers.ExactMatch)) {
                    predicate = predicate.And(x => x.Content.ToLower().Contains(request.Contains.MergeContent()));
                } else {
                    request.Contains.ForEach(containedWord => {
                        predicate = predicate.And(x => x.Content.ToLower().Contains(containedWord.ToLower()));
                    });
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Game)) {
                predicate = predicate.And(x => x.Game.ToLower().Contains(request.Game.ToLower()));
            }

            if (request.From != null) {
                predicate = predicate.And(x => x.Date >= request.From);
            }

            if (request.To != null) {
                predicate = predicate.And(x => x.Date <= request.To);
            }

            IEnumerable<QuoteDto> quotes = _context.Quotes
                .AsExpandableEFCore()
                .Where(predicate)
                .Select(x => new QuoteDto {
                    QuoteId = x.Id,
                    QuoteContent = x.Content,
                    Game = x.Game,
                    Date = x.Date
                });
            if(request.Modifiers.Contains(CommandModifiers.RandomMatch)
                && quotes.Any()) {
                return new List<QuoteDto> { quotes.Random() };
            }
            return quotes.ToList();
        }
    }
}
