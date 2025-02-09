using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Resources {
    public static class ErrorMessageRes {
        private const string ErrorMessageEmote = "Pengun";
        public const string NoMatchingRecordsFound = $"No matching records found. {ErrorMessageEmote}";
        public const string EmptyQuery = $"The query must include at least one (1) non-null argument. {ErrorMessageEmote}";
        public const string FormatError = $"Unexpected format. {ErrorMessageEmote}";
        public const string UnauthorizedAccess = $"Unauthorized access. {ErrorMessageEmote}";
    }
}
