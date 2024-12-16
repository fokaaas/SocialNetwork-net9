namespace Data.Interfaces;

public interface IRepository<TEntity>
{
    Task AddAsync(TEntity entity);

    void Delete(TEntity entity);

    void Update(TEntity entity);
}