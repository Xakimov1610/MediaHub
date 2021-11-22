using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaHub.Entity;
using MediaHub.Model;
using MediaHub.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaHub.Controllers
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
        public async Task<IActionResult> PostAsync(IEnumerable<IFormFile> images)
        {
            var extensions = new string[] { ".jpg", ".png", ".svg", ".mp4", ".heic" };
            var fileSize = 5242880;
            if (images.Count() < 1 || images.Count() > 5)
            {
                return BadRequest("Can upload 1~5 files at a time");
            }
            foreach (var file in images)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!extensions.Contains(fileExtension))
                {
                    return BadRequest($"{fileExtension} format file not allowed!");
                }

                if (file.Length > fileSize)
                {
                    return BadRequest($"Max file size 5MB!");
                }
            }

            var image = images.Select(f =>
            {
                using var stream = new MemoryStream();
                return new Image(
                    data: stream.ToArray(),
                    title: f.FileName,
                    altText: f.Name);
            }).ToList();

            var result = await _mediaService.CreateAsync(image);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetImagesAsync(Guid id)
        {
            if (!await _mediaService.ExistsAsync(id))
            {
                return NotFound();
            }

            var image = await _mediaService.GetAsync(id);

            return File(new MemoryStream(image.Data), image.Title);
        }
    }
}