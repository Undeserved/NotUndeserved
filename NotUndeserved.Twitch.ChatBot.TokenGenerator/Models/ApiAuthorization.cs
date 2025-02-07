using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.TokenGenerator.Models {
    public class ApiAuthorization {
        public string Code { get; }

        public ApiAuthorization(string code) {
            Code = code;
        }
    }
}
