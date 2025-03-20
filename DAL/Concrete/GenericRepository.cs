using DAL.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly B2bContext _context = new B2bContext();
        private readonly DbSet<T> data;

        public GenericRepository()
        {
             data = _context.Set<T>();
        }

        public bool Delete(T p)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                data.Remove(p);
                _context.SaveChanges();

                dbContextTransaction.Commit();
                return true;
            }
        }

        public T? GetById(Guid id)
        {
            return data.Find(id);
        }

        public bool Insert(T p)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                data.Add(p);
                _context.SaveChanges();

                dbContextTransaction.Commit();
                return true;
            }
        }

        public List<T> List()
        {
            return data.ToList();
        }

        public bool Update(T p)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                _context.SaveChanges();
                dbContextTransaction.Commit();
                return true;
            }
        }

        public List<T> Where(Func<T, bool> predicate)
        {
            return data.Where(predicate).ToList();
        }
    }
}