using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MedPoint.Stores.Impl
{
    public class ImageStore : IImageStore
    {
        private AppSettings AppSettings { get; }

        public ImageStore(IOptions<AppSettings> options)
        {
            AppSettings = options.Value;
        }

        public async Task SaveImage(string imageName, byte[] image)
        {
            var imageDirectory = Path.Combine(AppSettings.ResourcePaths.Base, AppSettings.ResourcePaths.Images);
            Directory.CreateDirectory(imageDirectory);
            var imagePath = Path.Combine(imageDirectory, imageName);

            await File.WriteAllBytesAsync(imagePath, image);
        }

        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(AppSettings.ResourcePaths.Base, AppSettings.ResourcePaths.Images, imageName);
            File.Delete(imagePath);
        }
    }
}
