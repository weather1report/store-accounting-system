using store_accounting_system.Core.Interfaces;

namespace store_accounting_system.Core.Entities;

public class Product : IHaveIntId
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}