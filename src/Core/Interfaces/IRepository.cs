namespace store_accounting_system.Core.Interfaces;

public interface IRepository<T> where T : IHaveIntId
{
    public List<T> GetList(IFilter<T>? filter);
    public void Create(T entity);
    public void Update(T entity);
    public void DeleteById(int id);
}