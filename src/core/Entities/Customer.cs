using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities;

public class Customer : IHaveIntId
{
    public int Id { get; set; } = 0;
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime RegisterDate { get; set; }
    public virtual List<Order>? Orders { get; set; }
}