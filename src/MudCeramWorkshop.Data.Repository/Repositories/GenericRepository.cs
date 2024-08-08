using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Repository.Utils.Extensions;
using System.Linq.Expressions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class GenericRepository<TEntity, TId>(ApplicationDbContext context) : IGenericRepository<TEntity, TId> where TEntity : class
{
    public virtual async Task<TEntity?> Get(TId id, CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>()
            .FindAsync([id, cancellationToken], cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    public virtual async Task<ICollection<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>()
            .ToListAsyncWait(cancellationToken);
    }

    public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
    }


    public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    protected IQueryable<TEntity> AddSorting(IQueryable<TEntity> query, string sortDirection, string propertyName)
    {
        var param = Expression.Parameter(typeof(TEntity));
        var prop = Expression.PropertyOrField(param, propertyName);
        var sortLambda = Expression.Lambda(prop, param);

        Expression<Func<IOrderedQueryable<TEntity>>> sortMethod = sortDirection switch
        {
            "Ascending" when query.Expression.Type == typeof(IOrderedQueryable<TEntity>) => () => ((IOrderedQueryable<TEntity>)query).ThenBy<TEntity, object>(k => null!),
            "Ascending" => () => query.OrderBy<TEntity, object>(k => null!),
            "Descending" when query.Expression.Type == typeof(IOrderedQueryable<TEntity>) => () => ((IOrderedQueryable<TEntity>)query).ThenByDescending<TEntity, object>(k => null!),
            "Descending" => () => query.OrderByDescending<TEntity, object>(k => null!),
            _ => () => query.OrderByDescending<TEntity, object>(k => null!),
        };

        var methodCallExpression = (sortMethod.Body as MethodCallExpression);

        if (methodCallExpression == null)
            throw new ArgumentException("MethodCallExpression null");

        var method = methodCallExpression.Method.GetGenericMethodDefinition();
        var genericSortMethod = method.MakeGenericMethod(typeof(TEntity), prop.Type);

        var orderQuery = genericSortMethod.Invoke(query, [query, sortLambda]) as IOrderedQueryable<TEntity>;

        if (orderQuery is null)
            throw new ArgumentException("MethodCallExpression null");

        return orderQuery;
    }
}