using MediatR;
using Microsoft.EntityFrameworkCore;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Quotes.Queries {
    public class GetLastQuoteQuery : IRequest<int> {
    }

    public class GetLastQuoteHandler : IRequestHandler<GetLastQuoteQuery, int> {
        private readonly IQuoteDatabaseContext _context;

        public GetLastQuoteHandler(IQuoteDatabaseContext context) {
            _context = context;
        }

        public async Task<int> Handle(GetLastQuoteQuery request, CancellationToken cancellationToken) {
            return await _context.Quotes.MaxAsync(x => x.Id);
        }
    }
}
