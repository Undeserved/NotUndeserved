using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using NotUndeserved.Twitch.ChatBot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.MessageParsers {
    public class MessageSanitiser {
        public readonly Dictionary<string,string> _wordMap = new Dictionary<string,string>();
        private Regex _regex;

        public MessageSanitiser(IQuoteDatabaseContext context) {
            LoadWords(context);
            BuildRegex();
        }

        private void LoadWords(IQuoteDatabaseContext context) {
            _wordMap.Clear();
            foreach (WordReplacement wordReplacement in context.WordReplacement) {
                _wordMap[wordReplacement.OriginalText.ToLowerInvariant()] = wordReplacement.ReplacementText;
            }
        }

        private void BuildRegex() {
            if(_wordMap.Count == 0) {
                _regex = new Regex("$^");
                return;
            }

            var pattern = $@"\b({string.Join("|", _wordMap.Keys.Select(Regex.Escape))})\b";
            _regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public string Sanitise(string input) {
            return _regex.Replace(input, match => {
                string originalWord = match.Value.ToLowerInvariant();
                return _wordMap.TryGetValue(originalWord, out string replacement) 
                ? replacement : originalWord;
            });
        }
    }
}
