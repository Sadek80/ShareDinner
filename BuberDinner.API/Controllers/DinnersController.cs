using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class DinnersController : ApiController
    {
        [HttpGet]
        public async ValueTask<IActionResult> GetDinners()
        {
            await Task.CompletedTask;
            return Ok(Array.Empty<string>());
        }
    }
}
