using Backend.DTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IAuthInterface
    {
        Task<UserDTO>? RegisterUser(UserDTO dto);
        Task<UserDTO>? AuthUser(UserDTO dto);
    }
}
