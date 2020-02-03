﻿ namespace Hastnama.Ekipchi.Common.General
{
    public class PagingOptions
    {
        public int Page { get; set; } = 0;

        public int Limit { get; set; } = 10;

        public string OrderBy { get; set; } = "default";

        public bool Desc { get; set; } = false;

    }
}