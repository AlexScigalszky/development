using System.Collections.Generic;

namespace Example.Model.Repositories
{
    public interface IGenericRepository<DTOEntity, TEntity> where TEntity : class//DTOEntity DTO to return, TEntity Entity to query
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<DTOEntity> GetAllDTO();
        TEntity Get(long id);
        DTOEntity GetDTO(long id);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges();
        TEntity GetDTOFromEntity(TEntity entity);
    }
}
