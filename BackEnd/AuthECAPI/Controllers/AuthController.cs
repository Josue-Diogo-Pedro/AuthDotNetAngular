using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthECAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthECAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IOptions<AppSettings> _options;
    
    public AuthController(UserManager<AppUser> userManager, IOptions<AppSettings> options)
    {
        _userManager = userManager;
        _options = options;
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<ActionResult> SignUp(UserRegistrationModel userRegistrationModel)
    {
        AppUser user = new(){
            UserName = userRegistrationModel.Email,
            Email = userRegistrationModel.Email,
            FullName = userRegistrationModel.FullName,
            Gender = userRegistrationModel.Gender,
            DOB = DateOnly.FromDateTime(DateTime.Now.AddYears(-userRegistrationModel.Age)),
            LibraryID = userRegistrationModel.LibraryID
        };

        var result = await _userManager.CreateAsync(user, userRegistrationModel.Password);
        await _userManager.AddToRoleAsync(user, userRegistrationModel.Role);

        if(result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<ActionResult> SignIn(LoginModel loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);

        if(user is not null & (await _userManager.CheckPasswordAsync(user!, loginModel.Password)))
        {
            var roles = await _userManager.GetRolesAsync(user!);
            var signInkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.JWTSecret));

            ClaimsIdentity claims = new(new Claim[]{
                new Claim("userID", user!.Id.ToString()),
                new Claim("gender", user.Gender.ToString()),
                new Claim("age", (DateTime.Now.Year - user.DOB.Year).ToString()),
                new Claim(ClaimTypes.Role, roles.First())
            });

            if(user.LibraryID != null)
                claims.AddClaim(new Claim("libraryID", user.LibraryID.ToString()!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    signInkey,
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return Ok(new {token});
        }
        else
            return BadRequest(new {message = "Username or password is incorrect."});
    }
}