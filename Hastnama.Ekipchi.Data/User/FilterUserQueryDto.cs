using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.Data.User
{
    public class FilterUserQueryDto
    {
        public string Keyword { get; set; }
        public UserStatus? Status { get; set; }
        public Common.Enum.Role? Role { get; set; }
    }
}