using System.ComponentModel.DataAnnotations;

namespace project1.Models
{
    public class User
    {
        public int id { get; set; }
        [MinLength(3)]
        [MaxLength(40)]
        public string name { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MinLength(5)]
        public string password { get; set; }
        public string Role { get; set; }
    }
}
