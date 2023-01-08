namespace Backend.DTOs
{
    public class UserDTO: ErrorDTO
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public int? RolId { get; set; }
        public RolDTO? Roles { get; set; }

        public bool? Status { get; set; }
        public string? Token { get; set; }
    }
}
