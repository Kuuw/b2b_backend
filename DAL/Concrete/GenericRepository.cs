using DAL.Abstract;
using Entities.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public List<T> Where(List<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var query = data.AsQueryable();
            if (include != null)
            {
                query = include(query);
            }
            foreach (var pred in predicate)
            {
                query = query.Where(pred).AsQueryable();
            }
            return query.ToList(); 
        }

        public List<T> GetPaged(int page, int pageSize, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var query = data.AsQueryable();
            if (include != null)
            {
                query = include(query);
            }
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        {
            {
        }
    }
}