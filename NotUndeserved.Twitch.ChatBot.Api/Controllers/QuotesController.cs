using Microsoft.AspNetCore.Mvc;
using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using NotUndeserved.Twitch.ChatBot.Application.Quotes.Commands;
using NotUndeserved.Twitch.ChatBot.Application.Quotes.Queries;

namespace NotUndeserved.Twitch.ChatBot.Api.Controllers {
    public class QuotesController : BaseController {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<QuoteDto>>> GetAll() {
            return Ok(await Mediator.Send(new GetQuoteQuery { From = DateTime.MinValue }));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<QuoteDto>> GetQuote(string Keyphrase) {
            GetQuoteQuery query = new GetQuoteQuery { Contains = new List<string>() { Keyphrase } };
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpsertQuote(UpsertQuoteCommand command) {
            await Mediator.Send(command);
            return Ok();
        }
    }
}
