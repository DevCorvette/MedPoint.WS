using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MedPoint.Services
{
    public interface IImageService
    {
        string GetImageUrl(string imageName);
        FileStreamResult GetImage(string imageName);
        Task<string> EditImage(string oldImageName, string image);
    }
}
