using Microsoft.AspNetCore.Identity;

namespace QuizApi.Data
{
    public static class SeedRoles
    {
        public static async Task SeedAdmin(IServiceProvider serviceProvider)
        {
            await Seed(serviceProvider, Roles.Admin);
        }

        public static async Task SeedParticipant(IServiceProvider serviceProvider)
        {
            await Seed(serviceProvider, Roles.Participant);
        }

        private static async Task Seed(IServiceProvider serviceProvider, Roles role)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (!await roleManager.RoleExistsAsync(role.ToString()))
            {
                var newRole = new IdentityRole(role.ToString());
                await roleManager.CreateAsync(newRole);
            }
        }
    }
}