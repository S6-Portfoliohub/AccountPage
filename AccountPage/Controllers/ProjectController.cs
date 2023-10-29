using Microsoft.AspNetCore.Mvc;

namespace AccountPage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> GetAllProjectsFromUser()
        {
            return Ok();
        }
    }
}
