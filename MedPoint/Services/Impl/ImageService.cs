using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MedPoint.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MedPoint.Services.Impl
{
    public class ImageService : IImageService
    {
        private IImageStore ImageStore { get; }
        private AppSettings AppSettings { get; }

        public ImageService(
            IImageStore imageStore,
            IOptions<AppSettings> options)
        {
            ImageStore = imageStore;
            AppSettings = options.Value;
        }

        public string GetImageUrl(string imageName)
        {
            return $"{AppSettings.Urls.Images}/{imageName}";
        }

        public FileStreamResult GetImage(string imageName)
        {
            var imagePath = Path.Combine(AppSettings.ResourcePaths.Base, AppSettings.ResourcePaths.Images, imageName);
            var stream = new MemoryStream(File.ReadAllBytes(imagePath));

            return new FileStreamResult(stream, "image/jpg");
        }

        public async Task<string> EditImage(string oldImageName, string image)
        {
            var imageName = $"{Guid.NewGuid()}.jpg";
            try
            {
                var base64Image = Convert.FromBase64String(image);
                await ImageStore.SaveImage(imageName, base64Image);
            }
            catch
            {
                return oldImageName;
            }

            if (!string.IsNullOrWhiteSpace(oldImageName))
            {
                ImageStore.DeleteImage(oldImageName);
            }

            return imageName;
        }
    }
}
