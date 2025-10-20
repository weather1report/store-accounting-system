using System.Linq.Expressions;

namespace store_accounting_system.core.Interfaces;

public abstract class IFilter<T>
{
    public abstract Expression<Func<T, bool>>? GetExpression(); 
    public virtual string? IncludeProperties => null;
    public virtual bool HasAnyCriteria() => GetExpression() != null;
}