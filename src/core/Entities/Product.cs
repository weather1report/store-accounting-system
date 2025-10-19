using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities;

public class Product : IHaveIntId
{
    public int Id { get; set; } = 0;
    public string? Name { get; set; }
    
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}