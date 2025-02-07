using Microsoft.AspNetCore.Mvc;
using NotUndeserved.Twitch.ChatBot.Application.Clips.Queries;

namespace NotUndeserved.Twitch.ChatBot.Api.Controllers {
    public class ClipsController : BaseController {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetRandomClip(string TargetChannel) {
            return Ok(await Mediator.Send(new GetRandomClipQuery { Channel = TargetChannel }));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetClip(string? Game) {
            return Ok(await Mediator.Send(new GetClipByParamsQuery { Game = Game }));
        }
    }
}
