namespace Entities.DTO;

public partial class CategoryGetDto
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryDescription { get; set; }
}
