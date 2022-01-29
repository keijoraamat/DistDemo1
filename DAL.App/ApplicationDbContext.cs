using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{

    public DbSet<ListItem> ListItems { get; set; } = default!;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}