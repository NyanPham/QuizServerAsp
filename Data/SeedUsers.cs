using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using QuizApi.Models;

namespace QuizApi.Data
{
    public static class SeedUsers
    {
        public static async Task<IActionResult> Seed(IServiceProvider serviceProvider, string email, string password, Roles role)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByEmailAsync(email);
            var roleExists = await roleManager.RoleExistsAsync(role.ToString());

            if (user == null && roleExists)
            {
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email
                };

                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    return new BadRequestObjectResult(result.Errors);
                }

                await userManager.AddToRoleAsync(user, role.ToString());
            }

            return new OkResult();
        }

        public static async Task SeedParticipant(IServiceProvider serviceProvider, string email)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var participant = new Participant
                {
                    Email = email,
                    Name = "Default Participant",
                    Score = 0,
                    TimeTaken = 0,
                    UserId = user.Id
                };

                context.Participants.Add(participant);
                await context.SaveChangesAsync();
            }
        }
    }
}