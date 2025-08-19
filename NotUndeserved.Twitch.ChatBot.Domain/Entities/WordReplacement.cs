using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Domain.Entities {
    public class WordReplacement {
        public int Id { get; set; }
        public string OriginalText { get; set; }
        public string ReplacementText { get; set; }
    }
}
