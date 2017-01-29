using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IRepository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Return all instances of type T.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> All();

        /// <summary>
        /// Return all instances of type T that match the expression exp.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicat);



        /// <summary>Returns the single entity matching the expression.
        /// Throws an exception if there is not exactly one such entity.</summary>
        /// <param name="exp"></param><returns></returns>
        T First(Expression<Func<T, bool>> predicat);

        /// <summary>
        /// Mark an entity to be deleted when the context is saved.
        /// </summary>
        /// <param name="entity"></param>
        T MarkForDeletion(T entity);

        /// <summary>
        /// Adds an entity of type T
        /// </summary>
        /// <param name="entity"></param>
        T InsertInstance(T entity);

        /// <summary>
        /// Update an entity of type T
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>Persist the data context.</summary>
        void Save();
    }
}
