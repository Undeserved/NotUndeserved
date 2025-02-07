using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Common.Dtos {
    public class ClipDto {
        public string Game {  get; set; }
        public string Broadcaster { get; set; }
        public string ClipUrl {  get; set; }
    }
}
