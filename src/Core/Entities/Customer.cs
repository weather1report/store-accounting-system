using store_accounting_system.Core.Interfaces;

namespace store_accounting_system.Core.Entities;

public class Customer : IHaveIntId
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime RegisterDate { get; set; }
    public virtual List<Order>? Orders { get; set; }
}