using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Data.Role;
using Hastnama.Ekipchi.Data.User.Wallet;

namespace Hastnama.Ekipchi.Data.User
{
    public class AdminUpdateUserDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FullName => $"{Name} {Family}";
        public string Username { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public UserStatus Status { get; set; }
        public bool Gender { get; set; }
        public List<int> Roles { get; set; }
    }
}