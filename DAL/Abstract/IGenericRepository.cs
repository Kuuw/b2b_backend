namespace DAL.Abstract
{
    public interface IGenericRepository<T>
    {
        List<T> List();
        bool Insert(T p);
        bool Delete(T p);
        bool Update(T p);
        T? GetById(Guid id);
        List<T> Where(Func<T, bool> predicate);
        public List<T> GetPaged(int page, int pageSize);
    }
}