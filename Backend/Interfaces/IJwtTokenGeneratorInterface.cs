using Backend.DTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IJwtTokenGeneratorInterface
    {
        string GenerateJwtToken(UserDTO dto);
    }
}
