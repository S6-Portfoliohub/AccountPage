using AccountPage.Models;
using DAL.DAO;
using DAOInterfaces.DTO;
using DAOInterfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountPage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectDAL _projectDAL;

        public ProjectController(IProjectDAL projectDAL)
        {
            _projectDAL = projectDAL;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<ProjectViewModel>>> GetAllProjectsFromUser(string userId)
        {
            List<ProjectDTO> projectDTOs = await _projectDAL.GetAllProjectsFromUser(userId);
            List<ProjectViewModel> project = projectDTOs.Select(x => new ProjectViewModel() { Id = x.Id, Description = x.Description, Name = x.Name, Img = x.Img }).ToList();

            return project;
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> CreateProject(ProjectViewModel project)
        {
            await _projectDAL.AddProjectForUser(new() { Name = project.Name, Description = project.Description, UserId = "test"});
            return Ok();
        }
    }
}
