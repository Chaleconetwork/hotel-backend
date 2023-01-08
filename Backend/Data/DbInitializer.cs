using Backend.Models;
using Microsoft.EntityFrameworkCore;
using static Library.Utils;

namespace Backend.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(Context context)
        {
            if (false)
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
                await context.Users.AddAsync(new User() { UserId = 1, Username = "chaleco", Password = Encrypt("1234"), RolId = 1, Status = true });
                await context.Users.AddAsync(new User() { UserId = 2, Username = "junior", Password = Encrypt("12345"), RolId = 2, Status = true });
            }

            if (!await context.Rols.AnyAsync())
            {
                await context.Rols.AddAsync(new Rol() { RolId = 1, RolName = "administrator" });
                await context.Rols.AddAsync(new Rol() { RolId = 2, RolName = "reception" });
            }

            await context.SaveChangesAsync();
        }
    }
}
