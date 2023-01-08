using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UserRepository: IUserInterface
    {
        public readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<User>>? GetUsers()
        {
            var users = await _context.Users.OrderBy(i => i.UserId).AsNoTracking().ToListAsync();

            return users;
        }
    }
}
