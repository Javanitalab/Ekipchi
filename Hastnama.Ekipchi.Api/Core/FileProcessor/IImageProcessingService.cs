using System.IO;

namespace Hastnama.Ekipchi.Api.Core.FileProcessor
{
    public interface IImageProcessingService
    {
        bool MakeThumbnail(Stream fileStream, string path);

        bool MakeThumbnail(Stream fileStream, string path, Orientation orientation);
    }
}