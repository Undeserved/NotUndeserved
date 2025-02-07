using NotUndeserved.Twitch.ChatBot.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Common.Dtos {
    public class QuoteDto {
        public int QuoteId { get; set; }
        public string QuoteContent { get; set; }
        public string Game { get; set; }
        public DateTime Date { get; set; }

        public string FormattedQuote => $"Quote #{QuoteId} {QuoteContent} [{Game}] [{Date.ToISO8601String()}]";
    }
}
