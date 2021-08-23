using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portal.Domain.Core.Auth;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Infrastructure.EF
{
    public class BookDbContextSeed
    {
        public static void SeedAsync(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<BookDbContext>();

            string[] roles = new string[] { "Administrator", "Manager", "Staff", "User" };
            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    var newRole = new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    };
                    roleStore.CreateAsync(newRole).Wait();
                }
            }
            //context.SaveChangesAsync().Wait();

            var user = new User
            {
                FirstName = "Super",
                LastName = "Admin",
                Email = "xxxx@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(user, "Abc#1234");
                user.PasswordHash = hashed;

                var userStore = new UserStore<User>(context);
                var result = userStore.CreateAsync(user);
            }

            AssignRolesAsync(serviceScope.ServiceProvider, user.Email, roles).Wait();

            context.SaveChangesAsync().Wait();
        }

        private static async Task<IdentityResult> AssignRolesAsync(IServiceProvider serviceScope, string email, string[] roles)
        {
            UserManager<User> _userManager = serviceScope.GetService<UserManager<User>>();
            User user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}