using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities;

public class Supply : IHaveIntId
{
    public int Id { get; set; } = 0;
    public DateTime Date { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}