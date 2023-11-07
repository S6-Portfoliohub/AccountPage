using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAOInterfaces.DTO
{
    public class ProjectDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string UserId {  get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? Img { get; set; }
    }
}
