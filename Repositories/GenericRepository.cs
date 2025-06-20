﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext Context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();
    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Delete(int entity) => _dbSet.Remove(_dbSet.Find(entity) ?? throw new InvalidOperationException("Entity not found."));

    public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();

    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);

    public void Update(T entity) => _dbSet.Update(entity);

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);

}
