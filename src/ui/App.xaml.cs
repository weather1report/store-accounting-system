using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using store_accounting_system.core.Entities;
using store_accounting_system.core.Interfaces;
using store_accounting_system.core.Services;
using store_accounting_system.data.DataAccess.Entities;
using store_accounting_system.ui;

namespace store_accounting_system.ui;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        
        var db = new StoreDbContext();
        IStoreService storeService = new StoreService(db, 
            new SqLiteRepository<Customer>(db.Customers), 
            new SqLiteRepository<Order>(db.Orders),
            new SqLiteRepository<OrderItem>(db.OrderItems), 
            new SqLiteRepository<Product>(db.Products), 
            new SqLiteRepository<Supply>(db.Supplies));
        
        var window = new MainWindow(storeService);
        window.Show();
    }
}