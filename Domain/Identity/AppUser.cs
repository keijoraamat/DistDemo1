using Microsoft.AspNetCore.Identity;

namespace Domain.Identity;

public class AppUser : IdentityUser<Guid>
{
    public ICollection<ListItem>? ListItems { get; set; }
}