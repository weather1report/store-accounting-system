using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using store_accounting_system.core.Entities;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.ui.ViewModels;

public class MainViewModel
{
    public readonly IStoreService StoreService;

    public MainViewModel(IStoreService storeService)
    {
        StoreService = storeService;
    }
    
    public ObservableCollection<Customer> Customers { get; } = new();
    public ObservableCollection<Order> Orders { get; } = new();
    public ObservableCollection<Product> Products { get; } = new();
    public ObservableCollection<OrderItem> OrderItems { get; } = new();
    public ObservableCollection<Supply> Supplies { get; } = new();
}