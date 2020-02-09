using System;
using System.Drawing;
using System.IO;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Microsoft.Extensions.Options;

namespace Hastnama.Ekipchi.Api.Core.FileProcessor
{
    public class ImageProcessingService : IImageProcessingService
    {
        private readonly ThumbnailSize _options;

        public ImageProcessingService(IOptions<ThumbnailSize> options)
        {
            Argument.IsNotNull(() => options);

            _options = options.Value;
        }

        private static bool ThumbnailCallback()
        {
            return false;
        }

        /// <summary>
        /// Make thumbnail image with automatic orientation detection.
        /// </summary>
        /// <param name="fileStream">file stream</param>
        /// <param name="path">save path.</param>
        /// <returns>return true if make thumbnail is success. else return false.</returns>
        public bool MakeThumbnail(Stream fileStream, string path)
        {
            var img = Image.FromStream(fileStream);
            var size = img.Size;

            var height = size.Height;
            var width = size.Width;

            return MakeThumbnail(fileStream, path, height > width ? Orientation.Portrait : Orientation.Landscape);
        }

        /// <summary>
        /// Make thumbnail image.
        /// </summary>
        /// <param name="fileStream">file stream</param>
        /// <param name="path">save path.</param>
        /// <param name="orientation">image orientation for make thumbnail</param>
        /// <returns>return true if make thumbnail is success. else return false.</returns>
        public bool MakeThumbnail(Stream fileStream, string path, Orientation orientation)
        {
            try
            {
                var img = Image.FromStream(fileStream);

                Image dest;
                switch (orientation)
                {
                    case Orientation.Portrait:
                        dest = GetReducedImage(_options.Portrait.Width, _options.Portrait.Height, img);
                        break;

                    case Orientation.Landscape:
                        dest = GetReducedImage(_options.Landscape.Width, _options.Landscape.Height, img);
                        break;

                    case Orientation.Square:
                        dest = GetReducedImage(_options.Landscape.Width, _options.Landscape.Width, img);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
                }

                dest.Save(path);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private Image GetReducedImage(int width, int height, Image resourceImage)
        {
            try
            {
                var callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                var reducedImage = resourceImage.GetThumbnailImage(width, height, callback, IntPtr.Zero);

                return reducedImage;
            }
            catch
            {
                return null;
            }
        }
    }

    public enum Orientation
    {
        Portrait,
        Landscape,
        Square
    }
}