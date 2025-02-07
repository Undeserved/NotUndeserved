using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Models {
    public class TwitchChatSettings {
        public int MaxMessageLength { get; set; }
        public char ParameterIdentifier {  get; set; }
    }
}
