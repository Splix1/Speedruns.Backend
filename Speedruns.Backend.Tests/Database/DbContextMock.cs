

using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Tests.Database
{
    public class DbContextMock<TEntity> where TEntity : BaseEntity, new ()
    {
        private readonly DbContext _context;
        private readonly DbSetMock<TEntity> _dbSet;

        public DbContextMock() : this(new List<TEntity>()) { }

        public DbContextMock(List<TEntity> entities)
        {
            _dbSet = new DbSetMock<TEntity>(entities);

            _context = Substitute.For<DbContext>();
            _context.Set<TEntity>().Returns(_dbSet.BaseEntities);
        }

        public DbContext Context => _context;

        public DbSet<TEntity> BaseEntities => _dbSet.BaseEntities;
    }
}
