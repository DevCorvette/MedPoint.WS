using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MedPoint;
using MedPoint.Services;
using Microsoft.Extensions.Options;
using NFluent;
using Xunit;

namespace Tests.MedPoint.Services.Impl
{
    public class ImageServiceTests : BaseTest
    {
        private const string Base64Image = "R0lGODlhEAAQAMQfAPHKNhAOBlxSHq+gNPv18lpZWLisl/baQs60Wvz1WNCtNvPSO8i7nfjlSsa+qMa0hbWrR6mQSPrqTvfgRt/Uaa6ac4V8MuW8N/nuUzg0IurUUuXJSvDGMsCNEqiFNP8A/yH5BAEAAB8ALAAAAAAQABAAAAWf4CeKzoMgjzOuH6NN8HQcG8MyTS7l8rE8I1xDginuZIuF7aNpUCiJKAZC1SwuH4egwA0ILIFCJhDICAyPSUZcBnczHkAFoSYHIAlLZjxYACJ0FAICA1IRFgp+HBEPBxNDRRhHPgByDjOPQzyUHBwGHxsLHqOko4oKIgxJHgStBB6KF58iBgCiHbgAnRcVNxeVuhccCrMsHwYVEREVxSIhADs=";
        private IImageService Service { get; }
        private AppSettings AppSettings { get; }

        public ImageServiceTests()
        {
            Service = DefaultServiceProvider.GetService<IImageService>();
            AppSettings = DefaultServiceProvider.GetService<IOptions<AppSettings>>().Value;
        }

        [Fact]
        public async Task EditImage_SaveImage_FileIsExist()
        {
            //Act
            var imageName = await Service.EditImage(null, Base64Image);

            //Assert
            var imagePath = Path.Combine(AppSettings.ResourcePaths.Base, AppSettings.ResourcePaths.Images, imageName);
            Check.That(File.Exists(imagePath)).IsTrue();

            //Cleanup
            File.Delete(imagePath);
        }

        [Fact]
        public async Task EditImage_DeleteOldImage_OldFileNotExist()
        {
            //Arrange
            var oldImageName = await Service.EditImage(null, Base64Image);
            var oldImagePath = Path.Combine(AppSettings.ResourcePaths.Base, AppSettings.ResourcePaths.Images, oldImageName);

            //Act
            var newImageName = await Service.EditImage(oldImageName, Base64Image);

            //Assert
            Check.That(File.Exists(oldImagePath)).IsFalse();

            //Cleanup
            File.Delete(Path.Combine(AppSettings.ResourcePaths.Base, AppSettings.ResourcePaths.Images, newImageName));
        }
        
    }
}
