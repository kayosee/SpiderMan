using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace FontDecoder.Model
{
    [Table(nameof(User))]
    public class User
    {
        [Required]
        [MaxLength(64)]
        [Dapper.Contrib.Extensions.Key]
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
        [MaxLength (250)]
        public string Roles { get; set; }
        public User() { }
    }
}
