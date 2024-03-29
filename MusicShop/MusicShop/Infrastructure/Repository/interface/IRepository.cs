﻿using System.Linq.Expressions;

namespace MusicShop.Infrastructure.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);

    }
}