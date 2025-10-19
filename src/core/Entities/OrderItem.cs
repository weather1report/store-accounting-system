using System.ComponentModel.DataAnnotations.Schema;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities;

public class OrderItem : IHaveIntId
{
    public int Id { get; set; } = 0;
    public int OrderId { get; set; }
    public int Count { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order? Order { get; set; }
}