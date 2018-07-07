namespace Rockstars.DataAccess.Repositories
{
    public interface IRepository<T>
    {
        void Create(T entity);

        T Get(long id);
    }
}
