using System.ComponentModel.DataAnnotations;
using DSS_Backend.Models;

namespace DSS_Backend.Models
{
    public class Comment
    {
        public int id { get; set; }

        [Required]
        public string description { get; set; }

        public int articleId { get; set; }
        public Article Article { get; set; }
    }
}
