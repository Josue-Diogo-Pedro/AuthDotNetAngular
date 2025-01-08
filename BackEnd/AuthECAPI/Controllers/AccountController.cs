using AuthECAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthECAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public AccountController(UserManager<AppUser> userManager) => _userManager = userManager;

    #region Old implementation about ClaimsPrincipal
    // private readonly HttpContext _httpContext;
    
    // public AccountController(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    // {
    //     _userManager = userManager;
    //     _httpContext = httpContextAccessor?.HttpContext;
    // }
    #endregion

    [Authorize]
    [HttpGet("UserProfile")]
    public async Task<ActionResult> GetUserProfile()
    {
        // ClaimsPrincipal user = new();
        // user = _httpContext.User;

        string userID = User.Claims.First(x => x.Type == "userID").Value;
        var userDetails = await _userManager.FindByIdAsync(userID);

        return Ok(
            new{
                Email = userDetails?.Email,
                FullName = userDetails?.FullName
            }
        );
    }        
}