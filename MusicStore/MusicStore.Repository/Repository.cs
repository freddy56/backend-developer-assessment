using IRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;


public abstract class Repository<T> : IRepository<T>
      where T : class
{
    protected DbContext _entities;
    protected readonly IDbSet<T> _dbset;

    public Repository(DbContext context)
    {
        _entities = context;
        _dbset = context.Set<T>();
    }

    public virtual IEnumerable<T> All()
    {

        return _dbset.AsEnumerable<T>();
    }

    public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
    {

        IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
        return query;
    }

    public virtual T First(Expression<Func<T, bool>> predicate)
    {
        T query = _dbset.Where(predicate).First();
        return query;
    }

    public virtual T InsertInstance(T entity)
    {
        return _dbset.Add(entity);
    }

    public virtual T MarkForDeletion(T entity)
    {
        return _dbset.Remove(entity);
    }

    public virtual void Update(T entity)
    {
        _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
    }

    public virtual void Save()
    {
        _entities.SaveChanges();
    }

    public void Dispose()
    {
        if (_entities != null)
        {
            _entities.Dispose();
            _entities = null;
        }
    }
}