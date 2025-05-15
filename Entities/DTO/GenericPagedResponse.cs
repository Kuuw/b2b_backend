namespace Entities.DTO;

public partial class GenericPagedResponse<T> where T : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();
}
