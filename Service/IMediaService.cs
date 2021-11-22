using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MediaHub.Entity;

namespace MediaHub.Service
{
    public interface IMediaService
    {
        Task<bool> ExistsAsync(Guid id);
        Task<Image> GetAsync(Guid id);
        Task<List<Image>> GetAllAsync();
        Task<(bool IsSuccess, Exception Exception)> CreateAsync(Image image);
        Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id);
    }
}