namespace Logic.Models
{
    public class AccountDetailsModel
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public ProjectModel? ProjectViewModels { get; set; }
    }
}
