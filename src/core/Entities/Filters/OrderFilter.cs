using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities.Filters;

public class OrderFilter : IFilter<Order>
{
    public int? Id { get; set; }
    public int? CustomerId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public decimal? MinTotal { get; set; }
    public decimal? MaxTotal { get; set; }
    
    public string SortField { get; set; } = "Date";
    public bool Increasing { get; set; } = true;

    public override Expression<Func<Order, bool>>? GetExpression()
    {
        return o =>
            (!Id.HasValue || o.Id == Id.Value) &&
            (!CustomerId.HasValue || o.CustomerId == CustomerId.Value) &&
            (!DateFrom.HasValue || o.Date >= DateFrom.Value) &&
            (!DateTo.HasValue || o.Date <= DateTo.Value) &&
            (!MinTotal.HasValue || o.TotalAmount >= MinTotal.Value) &&
            (!MaxTotal.HasValue || o.TotalAmount <= MaxTotal.Value);
    }
}