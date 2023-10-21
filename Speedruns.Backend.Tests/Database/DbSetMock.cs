

using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Tests.Database
{
    public class DbSetMock<TEntity> where TEntity : BaseEntity, new ()
    {
        private readonly DbSet<TEntity> _dbSet;

        public DbSetMock() : this(new List<TEntity>()) { }

        public DbSetMock(List<TEntity> entities)
        {
            _dbSet = entities.AsQueryable().BuildMockDbSet();
            _dbSet.AsQueryable().Returns(_dbSet);

            _dbSet
                .When(x => x.AddAsync(Arg.Any<TEntity>(), Arg.Any<CancellationToken>()))
                .Do(callInfo =>
                {
                    var entity = callInfo.Arg<TEntity>();
                    entities.Add(entity);
                });

            _dbSet
                .When(x => x.Add(Arg.Any<TEntity>()))
                .Do(callInfo =>
                {
                    var entity = callInfo.Arg<TEntity>();
                    entities.Add(entity);
                });

            _dbSet
                .When(x => x.Remove(Arg.Any<TEntity>()))
                .Do(callInfo =>
                {
                    var entity = callInfo.Arg<TEntity>();
                    entities.Remove(entity);
                });

            _dbSet
                .FindAsync(Arg.Any<object[]>())
                .Returns(callInfo =>
                {
                    var x = callInfo.Arg<object[]>();
                    return entities.SingleOrDefault(y => y.Id.Equals((long)x[0]));
                });

            _dbSet
                .Find(Arg.Any<object[]>())
                .Returns(callInfo =>
                {
                    var x = callInfo.Arg<object[]>();
                    return entities.SingleOrDefault(y => y.Id.Equals((long)x[0]));
                });
        }

        public DbSet<TEntity> BaseEntities => _dbSet;
    }
}
