using Example.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Example.Model.Repositories
{
    public abstract class GenericRepository<DTOEntity, TEntity> : IGenericRepository<DTOEntity, TEntity> where DTOEntity : BaseDTO where TEntity : BaseModel
    {

        protected readonly Context context;
        protected readonly IMapper mapper;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#creating-the-unit-of-work-class
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null,
            bool AsNoTracking = false)
        {
            IQueryable<TEntity> query = dbSet;

            if (AsNoTracking)
            query = query.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
                if (skip.HasValue)
                {
                    query = query.Skip(skip.Value);
                }
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        protected virtual IQueryable<DTOEntity> GetQueryableDTO(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null,
            bool AsNoTracking = false)
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take, AsNoTracking).ToArray().Select(x => GetDTOFromEntity(x)).AsQueryable<DTOEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public TEntity Get(long id)
        {
            return dbSet.Find(id);
        }

        public DTOEntity GetDTO(long id)
        {
            return GetDTOFromEntity(dbSet.Find(id));
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public IQueryable<DTOEntity> GetAllDTO()
        {
            return GetAll().ToArray().Select(x => GetDTOFromEntity(x)).AsQueryable();
        }

        public BaseListDTO<DTOEntity> GetAllPaginate(int pageNumber, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            var entities = GetQueryable(orderBy: orderBy);
            var pagination = entities
            .OrderByDescending(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
            var countTotal = entities.Count();
            var items = pagination
            .ToArray()
            .Select(s => GetDTOFromEntity(s))
            .ToArray();
            return new BaseListDTO<DTOEntity>(countTotal, pageNumber, pageSize, items);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        protected abstract DTOEntity GetDTOFromEntity(TEntity entity);

        TEntity IGenericRepository<DTOEntity, TEntity>.GetDTOFromEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
