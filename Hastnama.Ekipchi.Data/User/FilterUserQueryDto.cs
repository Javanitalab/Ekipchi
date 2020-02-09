using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.User
{
    public class FilterUserQueryDto : PagingOptions
    {
        public string Keyword { get; set; }
        public UserStatus? Status { get; set; }
        public int? RoleId { get; set; }
    }
}