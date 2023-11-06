using Microsoft.AspNetCore.Mvc;
using DAL.DAO;
using AccountPage.Models;
using System.Xml.Linq;

namespace AccountPage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<ProjectViewModel>>> GetAllProjectsFromUser(string userId)
        {
            //ProjectDAL projectDAL = new();
            //await projectDAL.GetAllProjectsFromUser(userId);
            List<ProjectViewModel> project = new()
            {
                new() { Id = "test", Name = "test", Description = "test", Img = "test.png" },
                new() { Id = "test1", Name = "test1", Description = "test1", Img = "test1.png" }
            };

            return project;
        }
    }
}
