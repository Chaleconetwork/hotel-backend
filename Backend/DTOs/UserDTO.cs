namespace Backend.DTOs
{
    public class UserDTO: ErrorDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Rol { get; set; }
        public bool? Status { get; set; }
        public string? Token { get; set; }
    }
}
