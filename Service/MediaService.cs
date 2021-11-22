using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaHub.Entity;

namespace MediaHub.Service
{
    public class MediaService : IMediaService
    {
        public Task<(bool IsSuccess, Exception Exception, Image image)> CreateAsync(Image genre)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Image>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<(bool IsSuccess, Exception Exception)> IMediaService.CreateAsync(Image image)
        {
            throw new NotImplementedException();
        }
    }
}