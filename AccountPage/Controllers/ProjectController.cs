using Microsoft.AspNetCore.Mvc;
using DAL.DAO;

namespace AccountPage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        [HttpGet(Name = "GetAllProjectsFromUser")]
        public async Task<IActionResult> GetAllProjectsFromUser(string userId)
        {
            ProjectDAL projectDAL = new();
            projectDAL.GetAllProjectsFromUser(userId);
            return Ok();
        }
    }
}
