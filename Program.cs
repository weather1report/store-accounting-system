using store_accounting_system.Core.Entities;
using store_accounting_system.Core.Entities.Filters;
using store_accounting_system.Core.Services;
using store_accounting_system.DataAccess.Entities;

using (var context = new StoreDbContext())
{
    StoreService service = new StoreService(context);
    foreach (var customer in service.GetList<Customer>())
    {
        Console.WriteLine(customer.Name);
    }
}