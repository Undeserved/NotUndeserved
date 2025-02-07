using NotUndeserved.Twitch.ChatBot.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands {
    public class RollDice : BaseChatCommand {
        private readonly Regex _diceRegex;
        private readonly Random _diceRng;
        private readonly IEnumerable<string> reservedNumbers;

        public RollDice() {
            CommandRegexPattern = "^[dD](?<Sides>[0-9]+)$";
            _diceRegex = new Regex(CommandRegexPattern);
            _diceRng = new Random();
            reservedNumbers = new List<string> {
                "8",
                "20",
                "420"
            };
        }

        public override async Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs) {
            string Sides = _diceRegex.Match(chatCommandReceivedArgs.Command.CommandText)
                .Groups[nameof(Sides)]
                .Value;

            if(reservedNumbers.Contains(Sides)
                || !int.TryParse(Sides, out int _sides)
                || _sides <= 0) {
                return string.Empty;
            }

            if (chatCommandReceivedArgs.Command.ArgumentsAsList.Any()) {
                IEnumerable<string> rolls = chatCommandReceivedArgs.Command.ArgumentsAsList
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => $"{x} rolled: {_diceRng.RollDice(_sides)}");
                return string.Join(", \t", rolls);
            }
            return $"D{_sides} rolled: {_diceRng.RollDice(_sides)}";
        }
    }
}
