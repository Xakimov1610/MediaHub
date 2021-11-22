using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Entity
{
    public class Image
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5242880)]
        public byte[] Data { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string AltText { get; set; }

        [Obsolete("Used only for Entities binding.", true)]
        public Image() { }

        public Image(byte[] data, string title, string altText)
        {
            Id = Guid.NewGuid();
            Data = data;
            Title = title;
            AltText = altText;
        }
    }
}