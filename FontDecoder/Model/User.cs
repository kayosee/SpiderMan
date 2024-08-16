using System.ComponentModel.DataAnnotations;

namespace FontDecoder.Model
{
    public class User
    {
        [Required]
        [MaxLength(64)]
        public string Username { get; set; }
        [Required]
        [MaxLength(64)]
        public string RealName { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
        public int Credit { get; set; }
        public User() { }
    }
}
