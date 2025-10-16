using System.ComponentModel.DataAnnotations.Schema;
using store_accounting_system.Core.Interfaces;

namespace store_accounting_system.Core.Entities;

public class Order : IHaveIntId
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int TotalAmount { get; set; }
    public DateTime Date { get; set; } 
    
    [ForeignKey("CustomerId")]
    public virtual Customer?  Customer { get; set; }
    public virtual List<OrderItem>? OrderItems { get; set; }
}