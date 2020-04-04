using System.Collections.Generic;
using System.Linq;

namespace Example.Model.Repositories
{
    public interface IGenericRepository<DTOEntity, TEntity> where DTOEntity : BaseDTO where TEntity : BaseModel
        //DTOEntity DTO to return, TEntity Entity to query
    {
        IQueryable<TEntity> GetAll();
        IQueryable<DTOEntity> GetAllDTO();
        BaseListDTO<DTOEntity> GetAllPaginate(int pageNumber, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        TEntity Get(long id);
        DTOEntity GetDTO(long id);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges();
        TEntity GetDTOFromEntity(TEntity entity);
    }
}
