using Logic.Models;

namespace Logic.Managers
{
    public interface IProjectManager
    {
        /// <summary>
        /// Creates a new project for a user
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        Task CreateProject(ProjectModel project);

        /// <summary>
        /// Method that retrives all projects from a user to show on their user page
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ProjectModel>> GetAllProjectsFromUser(string userId);

        /// <summary>
        /// Deletes all projects from a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeleteUser(string userId);
    }
}