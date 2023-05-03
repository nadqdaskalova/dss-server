using System.ComponentModel.DataAnnotations;

namespace DSS_Backend.Models
{
    public class User
    {
        public int id { get; set; }

        [Required]
        [MaxLength(50)]
        public string name { get; set; }

        [Required]
        [MaxLength(50)]
        public string email { get; set; }

        [Required]
        [MaxLength(50)]
        public string password { get; set; }
    }
}
