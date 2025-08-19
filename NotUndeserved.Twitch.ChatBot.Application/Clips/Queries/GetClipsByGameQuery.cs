using MediatR;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Clips.Queries {
    public class GetClipsByGameQuery : IRequest<List<ClipDto>> {
        public string Game {  get; set; }
    }

    public class GetClipsByGameHandler : IRequestHandler<GetClipsByGameQuery, List<ClipDto>> {
        private ITwitchApiService _twitchApiService;

        public GetClipsByGameHandler(ITwitchApiService twitchApiService) {
            _twitchApiService = twitchApiService;
        }

        public Task<List<ClipDto>> Handle(GetClipsByGameQuery request, CancellationToken cancellationToken) {
            return _twitchApiService.GetClipsByGame(request.Game);
        }
    }
}
