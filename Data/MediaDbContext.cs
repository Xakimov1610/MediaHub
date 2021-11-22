using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MediaHub.Entity;
using MediaHub.Model;
using MediaHub.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediaHub.Data
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaHubController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaHubController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(IEnumerable<ImageModel> images)
        {
            var extensions = new string[] { ".jpg", ".png", ".svg", ".mp4" };
            var fileSize = 5242880;
            if (images.Count() < 1 || images.Count() > 5)
            {
                return BadRequest("Can upload 1~5 files at a time");
            }
            foreach (var file in images)
            {
                var imagesExtension = Path.GetExtension(file.File.Name.ToLowerInvariant());
                if (!extensions.Contains(imagesExtension))
                {
                    return BadRequest($"{imagesExtension} format file not allowed!");
                }
                if (file.File.Length > fileSize)
                {
                    return BadRequest("Max file size 5MB");
                }
            }

            var image = images.Select(f =>
            {
                using var stream = new MemoryStream();
                return new Image(
                    data: stream.ToArray(),
                    title: f.Title,
                    altText: f.AltText);
            }).ToList();

            await _mediaService.CreateAsync(image);

            return Ok(); // <- bu chalaroq!
        }
    }
}