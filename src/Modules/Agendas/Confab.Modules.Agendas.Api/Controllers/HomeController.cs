using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    [ApiController]
    [ProducesDefaultContentType]
    [Route(AgendasModule.BasePath + "home")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => Ok("Conferences API");
    }
}