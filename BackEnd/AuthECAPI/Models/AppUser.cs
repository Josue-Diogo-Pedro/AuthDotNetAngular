using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AuthECAPI.Models;

public class AppUser: IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(150)")]
    public string FullName { get; set; } = string.Empty;

    [PersonalData]
    [Column(TypeName = "nvarchar(10)")]
    public string Gender {get; set;} = string.Empty;

    [PersonalData]
    public DateOnly DOB {get; set; }

    [PersonalData]
    public int? LibraryID {get; set; }
}