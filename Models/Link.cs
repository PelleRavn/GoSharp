using System;
using System.ComponentModel.DataAnnotations;

namespace GoSharp.Models
{
    public class Link
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Url { get; set; }

        public long Visits { get; set; } = 0L;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
