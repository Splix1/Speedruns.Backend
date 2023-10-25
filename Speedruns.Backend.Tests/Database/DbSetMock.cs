

using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Tests.Database
{
    /*
     * Similar to the DbContextMock, this uses generic type parameters
     * to say that we can mock anything as long as it is a BaseEntity.
     */
    public class DbSetMock<TEntity> where TEntity : BaseEntity, new ()
    {
        private readonly DbSet<TEntity> _dbSet;

        // Same as the DbContextMock, we use an empty list as the default if not provided.
        // This list acts as our fake DB.
        public DbSetMock() : this(new List<TEntity>()) { }

        public DbSetMock(List<TEntity> entities)
        {
            /* This is the "magic" of MockQueryable.NSubstitute that mocks the DbContext for us.
             * We can't easily do Substitue.For<DbSet<TEntity>>() without a lot of extra work
             * for reasons that don't matter right now.  So, we use that helpful library and call
             * .AsQueryable().BuildMockDbSet() instead so it can do all of that extra work for us
             * and we don't have to care about it YEP.
             */
            _dbSet = entities.AsQueryable().BuildMockDbSet();

            // This mocks the AsQueryable extenstion you can call on a DbSet to return the DbSet.

            _dbSet.AsQueryable().Returns(_dbSet);

             // This mocks out the AddAsync method.
            _dbSet
                /*
                 * This uses mocked parameters to the method.  The AddAsync method
                 * takes two parameters - an object to add, and a CancellationToken
                 * (something async methods need, don't worry about it too much).
                 * 
                 * The syntax of Arg.Any<TEntity>() is telling NSubstitute "accept any
                 * argument here for this parameter, as long as it matches the TEntity
                 * generic type parameter".  Phrased differently, this is how you tell
                 * NSubstitute that you don't care what is passed is as a parameter, as
                 * long as it matches a certain type.
                 * 
                 * Arg.Any<string>() says "accept any string as a parameter to this method".
                 */
                .When(x => x.AddAsync(Arg.Any<TEntity>(), Arg.Any<CancellationToken>()))

                /*
                 * This "When - Do" syntax in NSubstitute is how you mock methods with the "void"
                 * return type (methods that don't return anything).  You can do exactly the same
                 * logic with the regular "Returns()" method if an actual value needs to be returned
                 * (see the mocking of "Find" down below).
                 * 
                 * This syntax uses the "callInfo" argument which represents all of the praameters
                 * passed into the mocked method. callInfo represents them as an array in the order
                 * they were passed in.
                 * 
                 * callInfo[0] = our entity.
                 * callInfo[1] = our cancellation token.
                 * 
                 * You can also access the passed in parameter with the .Arg<>() syntax which will
                 * scan the array for the first parameter that matches the type in between the <>.
                 * So, this is scanning the parameters to get the entity we passed in.
                 * 
                 * This is the same as if we did
                 * "var entity = (TEntity)callInfo[0];"
                 * 
                 * This allows us to take the value passed into the mocked method and do something with it.
                 * In this case, we're taking the entity passed in to the mocked "AddAsync" method and we
                 * are adding it to the list of entities we passed in when creating this mocked DbSset.
                 * This is how we maintain a stateful DbSet that will allow us to test adding, updating, deleting,
                 * etc. methods.
                 */
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

            // Same as above, but for deleting.
            _dbSet
                .When(x => x.Remove(Arg.Any<TEntity>()))
                .Do(callInfo =>
                {
                    var entity = callInfo.Arg<TEntity>();
                    entities.Remove(entity);
                });

            /*
             * Similar to the above methods except these two methods mock the "Find" methods
             * which allows us to mock out returning an element from the "DB" (returning an element
             * from the list we're using to fake the data).
             * 
             * In this case, we "Find" the record in the DB by simply searching our list of
             * entities for the one that matches the Id provided. These methods take a weird
             * set of parameters (an object array), so the syntax here is to pull that array
             * of objects, and assume that the Id is the first one in the array.
             * 
             * This is a fine assumption since we control the data we're using in the tests.
             */
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

        // This just returns our mocked DbSet.
        public DbSet<TEntity> BaseEntities => _dbSet;
    }
}
