using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Utilities.DbContextSettings.Abstracts;

namespace Utilities.DbContextSettings
{
    /// <inheritdoc/>
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        protected DbContext DbContext { get; }

        /// <summary>
        /// Хранилище сущностей <typeparamref name="TEntity"/>.
        /// </summary>
        protected DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public EntityFrameworkRepository(DbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> AsQueryable()
        {
            return DbSet.AsQueryable<TEntity>();
        }

        /// <inheritdoc/>
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await DbContext.AddAsync(entity, cancellationToken);

            await SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(TEntity[] entities, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entities);

            await DbContext.AddRangeAsync(entities, cancellationToken);

            await SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            ArgumentNullException.ThrowIfNull(expression);
            return DbSet.Where(expression);
        }

        /// <inheritdoc />
        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var state = DbContext.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                DbContext.Attach(entity);
            }

            DbContext.Update(entity);

            return SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        private Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
