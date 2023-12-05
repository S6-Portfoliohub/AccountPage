namespace Logic.Models
{
    public class ProjectModel
    {
        public string? Id { get; set; }
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? Img { get; set; }
    }
}
