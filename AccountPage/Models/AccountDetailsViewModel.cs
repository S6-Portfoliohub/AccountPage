namespace AccountPage.Models
{
    public class AccountDetailsViewModel
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public List<ProjectViewModel>? Projects { get; set; }
    }
}
