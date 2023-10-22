using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Tests.Database
{
    /*
     * This uses something called Generic Type Parameters.
     * This is a concept where you want an object, or a method,
     * to take ANY parameters as long as they match a specific type.
     * Think like TS taking an `any` parameter to a function, except this
     * is a little more strict.
     * 
     * First, the <TEntity> notation represents the generic type.
     * The `TEntity` could be anything, think of it like a variable name.
     * You could have done <Splix> instead and it would have been fine.
     * The "T" is common for representing generic types (T for type),
     * so the most common use is to name a generic type parameter as
     * TSomething where "Something" is a general word representing what is
     * generic. In this case, we're saying "This DbContext can be mocked for
     * any entity", so TEntity is a fitting name.
     * 
     * The "where TEntity : BaseEntity, new()" is a type constraint.
     * Remember, a Generic Type Parameter can be literally any time, but sometimes
     * we don't want to be that generic, and we want to limit it to a specific 
     * sub-set of types.  In this case, we want to limit the generic type to be
     * only any type that extends the BaseEntity class.  This would be any of your
     * entities, and you can see this constraint in action by messing around.
     * 
     * Try to do DbContextMock<string>, it'll yell at you.
     * Take off the "where" clause here, and suddenly DbContextMock<string> won't error.
     * 
     * This is useful to ensure that when using generics, that you're not too generic as
     * to introduce bugs. It's helpful to allow only a sub-set of types that are all related
     * somehow (in this by all by entities).
     * 
     * The "new()" syntax is another restriction that says "ensure these types are classes that
     * can be instantiated". This helps eliminate built-in types that aren't custom classes you
     * write yourself.
     * 
     * Using a Generic Type Parameter here is helpful as it prevents you from needing to create
     * a DbContextMock or a DbSetMock for every single entity you have; that would be annoying
     * and a lot of duplicated code.
     */
    public class DbContextMock<TEntity> where TEntity : BaseEntity, new ()
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSetMock<TEntity> _dbSet;

        /*
         * The syntax " : this()" means that this constructor will call another
         * constructor on this object that takes the parameters. This allows us
         * to stack constructors on top of one another, which allows our object
         * to be created with different values (usually different default values).
         * 
         * In this case, if no List of default entities is provided, then we
         * simple create an empty list ourselves to be the default values,
         * and then we call the other constructor to continue on with constructing
         * everything.
         */
        public DbContextMock() : this(new List<TEntity>()) { }

        public DbContextMock(List<TEntity> entities)
        {
            _dbSet = new DbSetMock<TEntity>(entities);

            /*
             * This mocks the "Set" method on the DbContext.
             * This method is a type-safe way to access DbSets as calling it
             * will return the DbSet that matches the provided entity type.
             * 
             * So, _context.Set<ConsoleEntity>() will search the database context
             * and look for a corresponding DbSet<ConsoleEntity>, and if it finds one,
             * it will return it. Using this method is useful for mocking and can be
             * a little safer than accessing the property directly.
             */
            _context = Substitute.For<SpeedrunsContext>();
            _context
                .Set<TEntity>()
                .Returns(_dbSet.BaseEntities);
        }

        // This just returns the mocked DbContext.
        public SpeedrunsContext Context => _context;

        // This just returns the mocked DbSet.
        public DbSet<TEntity> BaseEntities => _dbSet.BaseEntities;
    }
}
