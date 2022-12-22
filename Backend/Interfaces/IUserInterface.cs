using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUserInterface
    {
        Task<List<User>>? GetUsers();
    }
}
