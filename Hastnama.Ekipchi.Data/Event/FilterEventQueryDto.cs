using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Event
{
    public class FilterEventQueryDto : PagingOptions
    {
        public string HostName { get; set; }

        public string Name { get; set; }
    }
}