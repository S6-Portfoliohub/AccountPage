﻿namespace AccountPage.Models
{
    public class ProjectViewModel
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? Img {  get; set; }
    }
}
