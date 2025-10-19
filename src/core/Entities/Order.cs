using System.ComponentModel.DataAnnotations.Schema;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities;

public class Order : IHaveIntId
{
    public int Id { get; set; } = 0;
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime Date { get; set; } 
    
    [ForeignKey("CustomerId")]
    public virtual Customer?  Customer { get; set; }
    public virtual List<OrderItem>? OrderItems { get; set; }
}