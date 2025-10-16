using Microsoft.EntityFrameworkCore;
using store_accounting_system.Core.Entities;
using store_accounting_system.Core.Interfaces;
using store_accounting_system.DataAccess.Entities;

namespace store_accounting_system.Core.Services;

public class StoreService(StoreDbContext context) : IStoreService
{
    private readonly DbContext _context = context;
    
    private readonly SqLiteRepository<Customer> _customerRepository = new SqLiteRepository<Customer>(context.Customers);
    private readonly SqLiteRepository<Order> _orderRepository = new SqLiteRepository<Order>(context.Orders);
    private readonly SqLiteRepository<OrderItem> _orderItemRepository = new SqLiteRepository<OrderItem>(context.OrderItems);
    private readonly SqLiteRepository<Product> _productRepository = new SqLiteRepository<Product>(context.Products);
    private readonly SqLiteRepository<Supply> _supplyRepository = new SqLiteRepository<Supply>(context.Supplies);

    private SqLiteRepository<T> GetRepository<T>() where T : class, IHaveIntId
    {
        if (typeof(T) == typeof(Customer))
            return (_customerRepository as SqLiteRepository<T>)!;
        if (typeof(T) == typeof(Order))
            return (_orderRepository as SqLiteRepository<T>)!;
        if (typeof(T) == typeof(OrderItem))
            return (_orderItemRepository as SqLiteRepository<T>)!;
        if (typeof(T) == typeof(Product))
            return (_productRepository as SqLiteRepository<T>)!;
        if (typeof(T) == typeof(Supply))
            return (_supplyRepository as SqLiteRepository<T>)!;
        
        throw new InvalidOperationException($"No repository registered for type {typeof(T).Name}");
    }
    
    private void SaveChanges()
    {
        _context.SaveChanges();
    }
    
    public void Add<T>(T entity) where T : class, IHaveIntId
    {
        GetRepository<T>().Create(entity);
        SaveChanges();
    }

    public void Update<T>(T entity) where T : class, IHaveIntId
    {
        GetRepository<T>().Update(entity);
        SaveChanges();
    }

    public void DeleteById<T>(int id) where T : class, IHaveIntId
    {
        GetRepository<T>().DeleteById(id);
        SaveChanges();
    }

    public List<T> GetList<T>(IFilter<T>? filter = null) where T : class, IHaveIntId
    {
        return GetRepository<T>().GetList(filter);
    }
}