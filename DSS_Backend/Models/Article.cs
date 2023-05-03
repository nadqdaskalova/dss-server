using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DSS_Backend.Models
{
    public class Article
    {
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string title { get; set; }

        [Required]
        [MaxLength(500)]
        public string image { get; set; }

        [Required]
        public string description { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
