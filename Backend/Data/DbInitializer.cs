using Backend.Models;
using Microsoft.EntityFrameworkCore;
using static Library.Utils;

namespace Backend.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(Context context)
        {
            if (true)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                await Seed(context);
            }
        }

        private static async Task Seed(Context context)
        {
            if (!await context.Users.AnyAsync())
            {
                await context.Users.AddAsync(new User() { Id = 1, Username = "chaleco", Password = Encrypt("1234"), Rol = "administrador", Status = true });
                await context.Users.AddAsync(new User() { Id = 2, Username = "junior", Password = Encrypt("12345"), Rol = "administrador", Status = true });
            }

            await context.SaveChangesAsync();
        }
    }
}
