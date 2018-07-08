using System.Collections.Generic;

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
        T Get(long id);

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
    }
}
