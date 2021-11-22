using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaHub.Data;
using MediaHub.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediaHub.Service
{
    public class MediaService : IMediaService
    {
        private readonly MediaDbContext _context;
        private readonly ILogger<MediaService> _logger;

        public MediaService(MediaDbContext context, ILogger<MediaService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, Exception Exception)> CreateAsync(IEnumerable<Image> images)
        {
            try
            {
                await _context.Images.AddRangeAsync(images);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Images created in DB.");
                return (true, null);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Creating image in DB failed.");
                return (false, e);
            }
        }

        public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id)
        {
            var image = await GetAsync(id);
            if (image == default)
            {
                return (false, new Exception("Not found."));
            }
            try
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Image deleted in DB. ID: {image.Id}");
                return (true, null);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Deleting image in DB failed.");
                return (false, e);
            }
        }

        public Task<bool> ExistsAsync(Guid id)
            => _context.Images.AnyAsync(i => i.Id == id);

        public Task<List<Image>> GetAllAsync()
            => _context.Images.ToListAsync();

        public Task<Image> GetAsync(Guid id)
            => _context.Images.FirstOrDefaultAsync(i => i.Id == id);
    }
}