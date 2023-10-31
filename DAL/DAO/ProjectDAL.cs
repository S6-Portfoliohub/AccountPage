using DAOInterfaces.DTO;
using DAOInterfaces.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL.DAO
{
    public class ProjectDAL : IProjectDAL
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public ProjectDAL()
        {
            string connectionUri = "mongodb+srv://tjerkzeil:<password>@portfoliohubcluster.d8rmxk5.mongodb.net/?retryWrites=true&w=majority";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);
            _client = client;
            _database = client.GetDatabase("PortfolioHubCluster");
        }

        public Task<List<ProjectDTO>> GetAllProjectsFromUser(string userId)
        {
            throw new NotImplementedException();
            try
            {
                var result = _client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public Task AddProjectForUser(ProjectDTO projectDTO)
        {
            var collection = _database.GetCollection<BsonDocument>("Projects");
            return collection.InsertOneAsync(projectDTO.ToBsonDocument());
        }

        public async Task EditProjectForUser(ProjectDTO projectDTO)
        {

        }

        public async Task DeleteProjectFromUser(string projectId)
        {

        }
    }
}
