using System;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.Data.User
{
    public class FilterUserQueryDto
    {
        public string Keyword { get; set; }
        public UserStatus? Status { get; set; }
        public int? RoleId { get; set; }
    }
}