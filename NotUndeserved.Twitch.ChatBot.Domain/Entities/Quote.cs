using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Domain.Entities {
    public class Quote {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Game { get; set; }
        public DateTime Date { get; set; }
    }
}
