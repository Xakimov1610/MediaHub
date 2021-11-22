using System.Net.Mime;
using MediaHub.Entity;
using Microsoft.EntityFrameworkCore;

namespace MediaHub.Data
{
    public class MediaDbContext : DbContext
    {
        public DbSet<Image> Images { get; set; }

        public MediaDbContext(DbContextOptions options)
            :base() { }
    }
}