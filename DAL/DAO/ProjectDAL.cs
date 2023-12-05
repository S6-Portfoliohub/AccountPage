using DAOInterfaces.DTO;
using DAOInterfaces.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DAL.DAO
{
    public class ProjectDAL : IProjectDAL
    {
        private readonly IMongoCollection<ProjectDTO> _projectCollection;

        public ProjectDAL(IOptions<ProjectDatabaseSettings> projectDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            projectDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                projectDatabaseSettings.Value.DatabaseName);

            _projectCollection = mongoDatabase.GetCollection<ProjectDTO>(
                projectDatabaseSettings.Value.ProjectCollectionName);
        }

        public async Task<List<ProjectDTO>> GetAllProjectsFromUser(string userId)
        {
            return await _projectCollection.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task AddProjectForUser(ProjectDTO projectDTO)
        {
            await _projectCollection.InsertOneAsync(projectDTO);
        }

        public async Task UpdateProjectForUser(ProjectDTO projectDTO)
        {
            await _projectCollection.ReplaceOneAsync(x => x.Id == projectDTO.Id, projectDTO);
        }

        public async Task DeleteProjectFromUser(string projectId)
        {
            await _projectCollection.DeleteOneAsync(x => x.Id == projectId);
        }
    }
}
