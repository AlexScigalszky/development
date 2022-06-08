using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using Example.Service.Models;

namespace Example.ServiceTests.Repositories.Base
{
    public class BaseRepositoryTest
    {
        protected void MockDataInDbContext<TDbContext, TEntity>(
            IQueryable<TEntity> data,
            Mock<TDbContext> _mockContext
        ) where TEntity : BaseModel where TDbContext : DbContext
        {
            MockDataInDbContext(data, _mockContext, c => c.Set<TEntity>());
        }

        protected void MockDataInDbContext<TDbContext, TEntity>(
            IQueryable<TEntity> data,
            Mock<TDbContext> _mockContext,
            Expression<Func<TDbContext, DbSet<TEntity>>> expression
        ) where TEntity : BaseModel where TDbContext: DbContext
        {
            var mockSet = new Mock<DbSet<TEntity>>();
            
            mockSet
                .As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet
                .As<IQueryable<TEntity>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet
                .As<IQueryable<TEntity>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet
                .As<IQueryable<TEntity>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());
            
            _mockContext
                .Setup(expression)
                .Returns(mockSet.Object);
        }
    }
}
