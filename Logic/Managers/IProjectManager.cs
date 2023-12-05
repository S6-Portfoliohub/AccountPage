using Logic.Models;

namespace Logic.Managers
{
    public interface IProjectManager
    {
        Task CreateProject(ProjectModel project);
        Task<List<ProjectModel>> GetAllProjectsFromUser(string userId);
    }
}