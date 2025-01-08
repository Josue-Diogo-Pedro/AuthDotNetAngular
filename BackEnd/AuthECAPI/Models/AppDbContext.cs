
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Models;

public class AppDbContext: IdentityDbContext
{
    public DbSet<AppUser>? AppUsers {get; set;}
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
}