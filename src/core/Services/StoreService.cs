using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using store_accounting_system.core.Entities;
using store_accounting_system.core.Entities.Filters;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Services;

public class StoreService(DbContext context, IRepository<Customer> customers, IRepository<Order> orders, IRepository<OrderItem> orderItems, IRepository<Product> products, IRepository<Supply> supplies) : IStoreService
{
    private readonly DbContext _context = context;

    private readonly IRepository<Customer> _customerRepository = customers;
    private readonly IRepository<Order> _orderRepository = orders;
    private readonly IRepository<OrderItem> _orderItemRepository = orderItems;
    private readonly IRepository<Product> _productRepository = products;
    private readonly IRepository<Supply> _supplyRepository = supplies;

    private IRepository<T> GetRepository<T>() where T : class, IHaveIntId
    {
        if (typeof(T) == typeof(Customer))
            return (_customerRepository as IRepository<T>)!;
        if (typeof(T) == typeof(Order))
            return (_orderRepository as IRepository<T>)!;
        if (typeof(T) == typeof(OrderItem))
            return (_orderItemRepository as IRepository<T>)!;
        if (typeof(T) == typeof(Product))
            return (_productRepository as IRepository<T>)!;
        if (typeof(T) == typeof(Supply))
            return (_supplyRepository as IRepository<T>)!;
        
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

    public void Add(Supply supply)
    {
        var products = GetRepository<Product>().GetList(new ProductFilter{Id = supply.ProductId});
        if (products.Count == 0)
            throw new Exception($"No product found while add Supply ProductId={supply.ProductId}");
        GetRepository<Supply>().Create(supply);
        var product = products.First();
        product.Quantity += supply.Quantity;
        GetRepository<Product>().Update(product);
        SaveChanges();
    }
    
    public void Add(Order order)
    {
        if (order.OrderItems == null)
        {
            GetRepository<Order>().Create(order);
            return;
        }
        
        foreach (var p in order.OrderItems)
        {
            var ps = GetRepository<Product>().GetList(new ProductFilter{Id = p.ProductId});
            if (ps.Count == 0)
                throw new Exception($"No product found while add Order ProductId={p.ProductId}");
            var product =  ps.First();
            if (product.Quantity < p.Count)
                throw new Exception($"The product with Id = {product.Id} is only available in this quantity: {product.Quantity}.");
            product.Quantity -= p.Count;
            GetRepository<Product>().Update(product);
        }
        GetRepository<Order>().Create(order);
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