using Microsoft.Extensions.Options;
using NotUndeserved.Twitch.ChatBot.Application.Common.Extensions;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using NotUndeserved.Twitch.ChatBot.Application.Models;
using NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands;
using NotUndeserved.Twitch.ChatBot.Infrastructure.Services.MessageParsers;
using System.Text.RegularExpressions;
using TwitchLib.Client.Interfaces;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services
{
    public class TwitchChatService {
        private readonly ITwitchClient _twitchClient;
        private readonly ITwitchApiSettingsService _twitchSettings;
        private readonly TwitchChatSettings _settings;
        private readonly IEnumerable<BaseChatCommand> _commands;
        private readonly QuoteScanner _quoteScanner;

        public TwitchChatService(IOptions<TwitchChatSettings> settings, ITwitchClient twitchClient, ITwitchApiSettingsService twitchSettings, QuoteScanner quoteScanner, IEnumerable<BaseChatCommand> commands) {
            _settings = settings.Value;
            _twitchSettings = twitchSettings;
            _twitchClient = twitchClient;
            _quoteScanner = quoteScanner;
            _commands = commands;

            _twitchClient.OnConnected += TwitchClient_OnConnected;
            _twitchClient.OnLog += TwitchClient_OnLog;
            _twitchClient.OnMessageReceived += TwitchClient_OnMessageReceived;
            _twitchClient.OnChatCommandReceived += TwitchClient_OnChatCommandReceived;

            if (!_twitchClient.Connect()) {
                Console.WriteLine($"Failed to connect to {_twitchSettings.TargetChannel}");
            }
        }

        private async void TwitchClient_OnChatCommandReceived(object? sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e) {
            BaseChatCommand? command = _commands
                .Where(x => Regex.Match(e.Command.CommandText, x.CommandRegexPattern).Success)
                .FirstOrDefault();

            if (command == null) {
                return;
            }

            string message = await command.ExecuteCommand(e);
            if (!string.IsNullOrWhiteSpace(message)) {
                _twitchClient.SendMessage(e.Command.ChatMessage.Channel, message);
            }
        }

        private async void TwitchClient_OnMessageReceived(object? sender, TwitchLib.Client.Events.OnMessageReceivedArgs e) {
            if(e.ChatMessage.Username.ToLower() != _twitchSettings.QuoteAuthority.ToLower()) {
                return;
            }

            await _quoteScanner.ParseMessage(e);
        }

        private void TwitchClient_OnLog(object? sender, TwitchLib.Client.Events.OnLogArgs e) {
            Console.WriteLine($"{e.DateTime.ToISO8601TimeString()}: {e.Data}");
        }

        private void TwitchClient_OnConnected(object? sender, TwitchLib.Client.Events.OnConnectedArgs e) {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }
    }
}
