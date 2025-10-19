using Microsoft.EntityFrameworkCore;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.data.DataAccess.Entities;

public class SqLiteRepository<T>(DbSet<T> set) : IRepository<T>  where T : class, IHaveIntId
{
    private readonly DbSet<T> _dbSet = set;

    public List<T> GetList(IFilter<T>? filter = null)
    {
        IQueryable<T> query = _dbSet;
        
        var expression = filter?.GetExpression();
        if (expression != null)
            query = query.Where(expression);
        
        if (!string.IsNullOrWhiteSpace(filter?.IncludeProperties))
        {
            foreach (var include in filter.IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                query = query.Include(include);
        }
        
        var orderBy = filter?.GetOrderBy();
        if (orderBy != null)
            query = orderBy(query);
        

        return query.ToList();
        
    }

    public void Create(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void DeleteById(int id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null) _dbSet.Remove(entity);
    }
}