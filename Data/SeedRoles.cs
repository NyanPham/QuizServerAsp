using Microsoft.AspNetCore.Identity;

namespace QuizApi.Data
{
    public class SeedRoles
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public SeedRoles(IServiceProvider serviceProvider)
        {
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        }

        public async Task SeedAdmin()
        {
            await Seed(Roles.Admin);
        }

        public async Task SeedParticipant()
        {
            await Seed(Roles.Participant);
        }

        private async Task Seed(Roles role)
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                var newRole = new IdentityRole(role.ToString());
                await _roleManager.CreateAsync(newRole);
            }

            return;
        }


    }
}