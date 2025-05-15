namespace Entities.DTO;

public partial class GenericPagedFilter<T> where T : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public T Filter { get; set; } = default!;
}
