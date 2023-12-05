using AccountPage.Models;
using Logic.Managers;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountPage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectManager _projectManager;

        public ProjectController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<ProjectViewModel>>> GetAllProjectsFromUser(string userId)
        {
            List<ProjectModel> projectDTOs = await _projectManager.GetAllProjectsFromUser(userId);
            List<ProjectViewModel> project = projectDTOs.Select(x => new ProjectViewModel() { Id = x.Id, Description = x.Description, Name = x.Name, Img = x.Img }).ToList();

            return project;
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> CreateProject(ProjectViewModel project)
        {
            await _projectManager.CreateProject(new() { Name = project.Name, Description = project.Description, UserId = "test", Img = project.Img });
            return Ok();
        }
    }
}
