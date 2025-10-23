﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public interface IGenericRepository<T> where T : class
    {

        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
       ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T entity);
        void UpdateAsync(T entity);
       void DeleteAsync(T entity);
    }
}
