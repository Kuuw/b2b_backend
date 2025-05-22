namespace Entities.DTO;

public class UserReportDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public double TotalSpent { get; set; }
    public double AverageSpent { get; set;}
    public DateTime? LastOrderDate { get; set; }
}
