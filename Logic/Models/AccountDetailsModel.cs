namespace Logic.Models
{
    public class AccountDetailsModel
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public List<ProjectModel>? ProjectViewModels { get; set; }
    }
}
