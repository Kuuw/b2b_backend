﻿using Entities.DTO;
using System.Linq.Expressions;

namespace DAL.Abstract
{
    public interface IGenericRepository<T>
    {
        List<T> List();
        bool Insert(T p);
        bool Delete(T p);
        bool Update(T p);
        T? GetById(Guid id);
        public List<T> Where(List<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> include = null);
        public List<T> GetPaged(int page, int pageSize, Func<IQueryable<T>, IQueryable<T>> include = null);
    }
}