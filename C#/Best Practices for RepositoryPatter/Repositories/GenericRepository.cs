using Example.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Example.Model.Repositories
{
    public abstract class GenericRepository<DTOEntity, TEntity> : IGenericRepository<DTOEntity, TEntity> where TEntity : class
    {

        readonly Context _context;
        readonly DbSet<TEntity> _dbSet;

        public Context Context => _context;
        public DbSet<TEntity> dbSet => _dbSet;

        public GenericRepository(Context context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
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

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<DTOEntity> GetAllDTO()
        {
            return GetAll().ToArray().Select(x => GetDTOFromEntity(x));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected abstract DTOEntity GetDTOFromEntity(TEntity entity);

        TEntity IGenericRepository<DTOEntity, TEntity>.GetDTOFromEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
