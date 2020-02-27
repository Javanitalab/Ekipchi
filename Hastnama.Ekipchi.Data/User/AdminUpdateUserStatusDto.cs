using System;
using System.Text.Json.Serialization;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.Data.User
{
    public class AdminUpdateUserStatusDto
    {
        public UserStatus Status { get; set; }
    }
}