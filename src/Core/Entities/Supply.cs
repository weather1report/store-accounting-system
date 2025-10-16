using store_accounting_system.Core.Interfaces;

namespace store_accounting_system.Core.Entities;

public class Supply : IHaveIntId
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}