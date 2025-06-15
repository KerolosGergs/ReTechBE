using Microsoft.AspNetCore.Identity;

namespace ReTechBE.UserDTO
{
    public class RoleSeeder
    {
        private static readonly string[] Roles = { "Admin", "Customer", "RecyclingCompany" };

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
