
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Domain.Views {
    [Keyless]
    public class vwWrappedFrequencies {
        public string Game { get; set; }
        public int Year { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public int QuoteCount { get; set; }
        public double Frequency { get; set; }
    }
}
