using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Data;
using QuizApi.DTOs;
using QuizApi.Models;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountController(IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
        if (model.Password != model.PasswordConfirm)
        {
            return BadRequest("Passwords do not match");
        }

        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Roles.Participant.ToString());
            await _signInManager.SignInAsync(user, isPersistent: false);

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (roles.Count == 0 || role == null)
            {
                return Unauthorized();
            }

            // Create and save the Participant entity
            var participant = new Participant
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id
            };

            var userInfo = new UserInfo
            {
                Email = user.Email,
                Username = user.UserName,
                Role = role
            };

            return Ok(new { CurrentUser = userInfo, Token = GenerateToken(user.Email, role), Result = "User registered and logged in successfully" });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (roles.Count == 0 || role == null)
            {
                return Unauthorized();
            }

            var userInfo = new UserInfo
            {
                Email = user.Email,
                Username = user.UserName,
                Role = role
            };

            return Ok(new { CurrentUser = userInfo, Token = GenerateToken(model.Email, role), Result = "User logged in successfully" });
        }

        return Unauthorized();
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Result = "User logged out successfully" });
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

        if (resetPassResult.Succeeded)
        {
            return Ok(new { Result = "Password reset successfully" });
        }

        return BadRequest(resetPassResult.Errors);
    }

    private string GetSecretKey()
    {
        var secretKey = _configuration["Jwt:Key"];
        return secretKey!;
    }

    private string GenerateToken(string email, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(GetSecretKey());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}