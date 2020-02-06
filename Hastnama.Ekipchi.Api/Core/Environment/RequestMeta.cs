using System;

namespace Hastnama.Ekipchi.Api.Core.Environment
{
    public class RequestMeta : IRequestMeta
    {
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public string Browser { get; set; }
        public string Os { get; set; }
        public string Device { get; set; }

        public Guid UserId { get; set; }
    }
}