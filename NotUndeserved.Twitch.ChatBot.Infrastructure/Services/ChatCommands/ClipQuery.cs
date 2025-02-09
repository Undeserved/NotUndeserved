using MediatR;
using NotUndeserved.Twitch.ChatBot.Application.Clips.Queries;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using NotUndeserved.Twitch.ChatBot.Infrastructure.Resources;
using TwitchLib.Client.Events;

namespace NotUndeserved.Twitch.ChatBot.Infrastructure.Services.ChatCommands {
    public class ClipQuery : BaseChatCommand {
        private IMediator _mediator;
        public ClipQuery(IMediator mediator) { 
            _mediator = mediator;
            CommandRegexPattern = "^clipquery( |$)";
        }

        public override async Task<string> ExecuteCommand(OnChatCommandReceivedArgs chatCommandReceivedArgs) {    
            if(!(chatCommandReceivedArgs.Command.ChatMessage.IsBroadcaster  || chatCommandReceivedArgs.Command.ChatMessage.IsModerator)) {
                return null;
            }

            string gameName = chatCommandReceivedArgs.Command.ArgumentsAsString;
            if (string.IsNullOrEmpty(gameName)) {
                return ErrorMessageRes.EmptyQuery;
            }

            var clipDto = await _mediator.Send(new GetClipByParamsQuery { Game = gameName });
            if (string.IsNullOrEmpty(clipDto.ClipUrl)) {
                return ErrorMessageRes.NoMatchingRecordsFound;
            }

            return $"Here's a random [{ clipDto.Game }] clip: { clipDto.ClipUrl }";
        }
    }
}
