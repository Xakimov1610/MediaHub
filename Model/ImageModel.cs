using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MediaHub.Model
{
    public class ImageModel
    {
        [Required]
        public IFormFile File { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }
        
        public string AltText { get; set; } 
    }
}