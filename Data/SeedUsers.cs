using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace QuizApi.Data
{
    public class SeedUsers
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;

        public SeedUsers(IServiceProvider serviceProvider)
        {
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        }

        public async Task<IActionResult> Seed(string Email, string Password, Roles role)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            var roleExists = await _roleManager.RoleExistsAsync(role.ToString());

            if (user == null && roleExists)
            {
                user = new IdentityUser
                {
                    UserName = Email,
                    Email = Email
                };

                var result = await _userManager.CreateAsync(user, Password);

                if (!result.Succeeded)
                {
                    return new BadRequestObjectResult(result.Errors);
                }


                await _userManager.AddToRoleAsync(user, role.ToString());
            }

            return new OkResult();
        }
    }
}