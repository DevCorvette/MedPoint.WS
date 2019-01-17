using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedPoint.Services;

namespace MedPoint.Controllers
{
    [EnableCors("cors")]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private IImageService ImageService { get; }

        public ImageController(IImageService imageService)
        {
            ImageService = imageService;
        }

        [HttpGet("GetImage/{imageName}")]
        public FileStreamResult GetImage(string imageName)
        {
            return ImageService.GetImage(imageName);
        }
    }
}
