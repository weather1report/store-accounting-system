using System.ComponentModel.DataAnnotations.Schema;
using store_accounting_system.Core.Interfaces;

namespace store_accounting_system.Core.Entities;

public class OrderItem : IHaveIntId
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int Count { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order? Order { get; set; }
}