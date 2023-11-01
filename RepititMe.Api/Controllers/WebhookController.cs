using Microsoft.AspNetCore.Mvc;
using RepititMe.Api.bot;
using Telegram.Bot.Types;

namespace RepititMe.Api.Controllers
{
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandlerService handleUpdateService,
                                      [FromBody] Update update)
        {
            await handleUpdateService.EchoAsync(update);
            return Ok();
        }
    }
}
