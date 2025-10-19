namespace store_accounting_system.core.Interfaces;

public interface IStoreService
{
    public void Add<T>(T entity) where T : class, IHaveIntId;
    public void Update<T>(T entity) where T : class, IHaveIntId;
    public void DeleteById<T>(int id) where T : class, IHaveIntId;
    public List<T> GetList<T>(IFilter<T>? filter) where T : class, IHaveIntId;
}