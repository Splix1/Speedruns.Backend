using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Tests.Database
{
    public class DbContextMock<TEntity> where TEntity : BaseEntity, new ()
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSetMock<TEntity> _dbSet;

        public DbContextMock() : this(new List<TEntity>()) { }

        public DbContextMock(List<TEntity> entities)
        {
            _dbSet = new DbSetMock<TEntity>(entities);

            _context = Substitute.For<SpeedrunsContext>();
            _context
                .Set<TEntity>()
                .Returns(_dbSet.BaseEntities);
        }

        public SpeedrunsContext Context => _context;

        public DbSet<TEntity> BaseEntities => _dbSet.BaseEntities;
    }
}
