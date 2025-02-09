using MediatR;
using NotUndeserved.Twitch.ChatBot.Application.Clips.Queries;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using NotUndeserved.Twitch.ChatBot.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands {
    public class RandomClip : BaseChatCommand {
        private IMediator _mediator;

        public RandomClip(IMediator mediator) {
            _mediator = mediator;
            CommandRegexPattern = "^randomclip( |$)";
        }

        public async override Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs) {
            if (!(chatCommandReceivedArgs.Command.ChatMessage.IsBroadcaster || chatCommandReceivedArgs.Command.ChatMessage.IsModerator)) {
                return null;
            }

            string targetChannel = chatCommandReceivedArgs.Command.ArgumentsAsString;
            if (string.IsNullOrEmpty(targetChannel)) {
                return ErrorMessageRes.EmptyQuery;
            }

            var clipDto = await _mediator.Send(new GetRandomClipQuery { Channel = targetChannel });
            if (string.IsNullOrWhiteSpace(clipDto.ClipUrl)) {
                return ErrorMessageRes.NoMatchingRecordsFound;
            }

            return $"Here's a random [{targetChannel}] clip: {clipDto.ClipUrl}";
        }
    }
}
