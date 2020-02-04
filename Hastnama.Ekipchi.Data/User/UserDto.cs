using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.Data.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Mobile { get; set; }
        public Common.Enum.Role? Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FullName => $"{Name} {Family}";
        public string Username { get; set; }
        public string Avatar { get; set; }
        public UserStatus Status { get; set; }
        public bool Gender { get; set; }
    }
}