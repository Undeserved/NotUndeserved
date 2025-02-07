using MediatR;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Clips.Queries {
    public class GetRandomClipQuery : IRequest<ClipDto> {
        public string Channel {  get; set; }
    }

    public class GetRandomClipHandler : IRequestHandler<GetRandomClipQuery, ClipDto> {
        private readonly ITwitchApiService _twitchApiService;

        public GetRandomClipHandler(ITwitchApiService twitchApiService) {
            _twitchApiService = twitchApiService;
        }

        public async Task<ClipDto> Handle(GetRandomClipQuery request, CancellationToken cancellationToken) {
            return await _twitchApiService.GetRandomClip(request.Channel);
        }
    }
}
