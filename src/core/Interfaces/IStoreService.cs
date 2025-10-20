using store_accounting_system.core.Entities;

namespace store_accounting_system.core.Interfaces;

public interface IStoreService
{
    public void Add<T>(T entity) where T : class, IHaveIntId;
    public void Add(Supply supply);
    public void Add(Order order);
    public void Update<T>(T entity) where T : class, IHaveIntId;
    public void DeleteById<T>(int id) where T : class, IHaveIntId;
    public List<T> GetList<T>(IFilter<T>? filter = null) where T : class, IHaveIntId;
}