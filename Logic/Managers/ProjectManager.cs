using DAOInterfaces.DTO;
using DAOInterfaces.Interfaces;
using Logic.Models;

namespace Logic.Managers
{
    public class ProjectManager : IProjectManager
    {
        private readonly IProjectDAL _projectDAL;
        public ProjectManager(IProjectDAL projectDAL)
        {
            _projectDAL = projectDAL;
        }

        public async Task<List<ProjectModel>> GetAllProjectsFromUser(string userId)
        {
            List<ProjectDTO> projectDTOs = await _projectDAL.GetAllProjectsFromUser(userId);
            List<ProjectModel> project = projectDTOs.Select(x => new ProjectModel() { Id = x.Id, Description = x.Description, Name = x.Name, Img = x.Img, UserId = x.UserId }).ToList();

            return project;
        }

        public async Task CreateProject(ProjectModel project)
        {
            await _projectDAL.AddProjectForUser(new() { Name = project.Name, Description = project.Description, UserId = project.UserId, Img = project.Img });
        }
    }
}
