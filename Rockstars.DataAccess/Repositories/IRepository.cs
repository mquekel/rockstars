using System;
using System.Collections.Generic;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);

        /// <summary>
        /// Creates multiple entities.
        /// </summary>
        /// <param name="entities"></param>
        void Create(IEnumerable<T> entities); 

        /// <summary>
        /// Get an entity by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="artist"></param>
        void Update(T artist);

        /// <summary>
        /// Search for an entity.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<T> Search(Func<Artist, bool> query);

        /// <summary>
        /// Delete the entity.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
