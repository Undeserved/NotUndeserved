using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Domain.Views {
    [Keyless]
    public class vwWrappedGameTotals {
        public string Game { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
    }
}
