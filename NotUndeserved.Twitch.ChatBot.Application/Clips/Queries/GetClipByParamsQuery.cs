using MediatR;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;

namespace NotUndeserved.Twitch.ChatBot.Application.Clips.Queries {
    public class GetClipByParamsQuery : IRequest<ClipDto> {
        public string Game {  get; set; }
    }

    public class GetClipByParamsHandler : IRequestHandler<GetClipByParamsQuery, ClipDto> {
        private ITwitchApiService _twitchApiService;
        private IQuoteDatabaseContext _quoteDatabaseContext;

        public GetClipByParamsHandler(ITwitchApiService switchApiService, IQuoteDatabaseContext quoteDatabaseContext) {
            _twitchApiService = switchApiService;
            _quoteDatabaseContext = quoteDatabaseContext;
        }

        public Task<ClipDto> Handle(GetClipByParamsQuery request, CancellationToken cancellationToken) {
            return _twitchApiService.GetClip(request.Game);
        }
    }
}
