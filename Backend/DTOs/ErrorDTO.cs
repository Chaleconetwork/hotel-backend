namespace Backend.DTOs
{
    public class ErrorDTO
    {
        public bool? IsError { get; set; } = false;
        public string? MessageError { get; set; } = string.Empty;
    }
}
