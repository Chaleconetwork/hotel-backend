using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        public string? RolName { get; set; }
    }
}
