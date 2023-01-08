using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public int? RolId { get; set; }
        public Rol? Roles { get; set; }

        public bool? Status { get; set; }
    }
}
