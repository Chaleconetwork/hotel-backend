using Backend.Models;
using Microsoft.EntityFrameworkCore;

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
                await context.Users.AddAsync(new User() { Id = 1, Username = "chaleco", Password = "1234", Rol = "administrador", Status = "activo" });
                await context.Users.AddAsync(new User() { Id = 2, Username = "junior", Password = "1234", Rol = "administrador", Status = "activo" });
            }

            await context.SaveChangesAsync();
        }
    }
}
