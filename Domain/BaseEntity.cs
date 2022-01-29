namespace Domain;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime UpdatedAt { get; set; }
}