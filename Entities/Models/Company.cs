namespace Entities.Models;

public partial class Company
{
    public Guid CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
