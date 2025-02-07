using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Domain.Entities {
    public class QuoteLog {
        public long LogEntry { get; set; }
        public int QuoteId { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
