using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities.Filters;

public class OrderItemFilter : IFilter<OrderItem>
{
    public int? Id { get; set; }
    public int? OrderId { get; set; }
    public int? ProductId { get; set; }
    
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    public override Expression<Func<OrderItem, bool>>? GetExpression()
    {
        return o =>
            (!Id.HasValue || o.Id == Id.Value) &&
            (!OrderId.HasValue || o.OrderId == OrderId.Value) &&
            (!ProductId.HasValue || o.ProductId >= ProductId.Value) &&
            (!DateFrom.HasValue || o.Date >= DateFrom.Value) &&
            (!DateTo.HasValue || o.Date <= DateTo.Value);
    }
}