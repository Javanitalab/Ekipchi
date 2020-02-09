namespace Hastnama.Ekipchi.Common.General
{
    public class PagingOptions
    {
        public bool NoPaging { get; set; }
        public int? Page { get; set; }

        public int? Limit { get; set; }

        public string OrderBy { get; set; } = "default";

        public bool Desc { get; set; } = false;
    }
}