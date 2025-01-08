using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthECAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthDemoController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("AdminOnly")]
    public string AdminOnly() => "Admin Only";

    [Authorize(Roles = "Admin,Teacher")]
    [HttpGet("AdminOrTeacher")]
    public string AdminOrTeacher() => "Admin or Teacher";

    [Authorize(Policy = "HasLibraryID")]
    [HttpGet("LibraryMembersOnly")]
    public string LibraryMembersOnly() => "Library members only";

    [Authorize(Roles = "Teacher", Policy = "FemalesOnly")]
    [HttpGet("ApplyForMaternityLeave")]
    public string ApplyForMaternityLeave() => "Applied for maternity leave.";

    [Authorize(Policy = "Under10")]
    [Authorize(Policy = "FemalesOnly")]
    [HttpGet("Under10AndFemale")]
    public string Under10AndFemale() => "Under 10 And Female";

}