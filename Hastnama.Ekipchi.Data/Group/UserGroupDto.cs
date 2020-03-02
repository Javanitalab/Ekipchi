using System;

namespace Hastnama.Ekipchi.Data.Group
{
    public class UserGroupDto
    {
        public Guid Id { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FullName => $"{Name} {Family}";
        public string Username { get; set; }
        public string Avatar { get; set; }
        public bool IsAdmin { get; set; }
    }
}