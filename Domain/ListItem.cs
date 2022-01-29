using Domain.Identity;

namespace Domain;

public class ListItem : BaseEntity
{
    public string Description { get; set; } = default!;
    public bool IsDone { get; set; }

    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}