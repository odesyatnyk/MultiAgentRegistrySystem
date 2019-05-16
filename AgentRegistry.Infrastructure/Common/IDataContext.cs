using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgentRegistry.Infrastructure.Common
{
    public interface IDataContext
    {
        DatabaseFacade Database { get; }

        ChangeTracker ChangeTracker { get; }

        IModel Model { get; }

        EntityEntry Add([NotNull] object entity);
        

        EntityEntry<TEntity> Add<TEntity>([NotNull] TEntity entity) where TEntity : class;
        
        void AddRange([NotNull] IEnumerable<object> entities);
        
        void AddRange([NotNull] params object[] entities);

        Task AddRangeAsync([NotNull] params object[] entities);
        
        EntityEntry<TEntity> Attach<TEntity>([NotNull] TEntity entity) where TEntity : class;
        
        EntityEntry Attach([NotNull] object entity);
        
        void AttachRange([NotNull] params object[] entities);
        
        void AttachRange([NotNull] IEnumerable<object> entities);
        
        void Dispose();
        
        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;
        
        EntityEntry Entry([NotNull] object entity);
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
        
        object Find([NotNull] Type entityType, [CanBeNullAttribute] params object[] keyValues);
        
        TEntity Find<TEntity>([CanBeNullAttribute] params object[] keyValues) where TEntity : class;
        
        Task<TEntity> FindAsync<TEntity>([CanBeNullAttribute] params object[] keyValues) where TEntity : class;
        
        Task<object> FindAsync([NotNull] Type entityType, [CanBeNullAttribute] object[] keyValues, CancellationToken cancellationToken);
        
        Task<TEntity> FindAsync<TEntity>([CanBeNullAttribute] object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        
        Task<object> FindAsync([NotNull] Type entityType, [CanBeNullAttribute] params object[] keyValues);
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();
        
        DbQuery<TQuery> Query<TQuery>() where TQuery : class;
        
        EntityEntry Remove([NotNull] object entity);
        
        EntityEntry<TEntity> Remove<TEntity>([NotNull] TEntity entity) where TEntity : class;
        
        void RemoveRange([NotNull] IEnumerable<object> entities);
        
        void RemoveRange([NotNull] params object[] entities);
        
        int SaveChanges(bool acceptAllChangesOnSuccess);
        
        int SaveChanges();
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
        
        EntityEntry Update([NotNull] object entity);
        
        EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity) where TEntity : class;
        
        void UpdateRange([NotNull] params object[] entities);
        
        void UpdateRange([NotNull] IEnumerable<object> entities);

    }
}
