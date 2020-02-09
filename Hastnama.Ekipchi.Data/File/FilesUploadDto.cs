using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Hastnama.Ekipchi.Data.File
{
    public class FilesUploadDto
    {
        public List<IFormFile> Files { get; set; }

        public string Type { get; set; }

        public bool IsPrivate { get; set; }

        public List<string> LocalId { get; set; }
    }
}