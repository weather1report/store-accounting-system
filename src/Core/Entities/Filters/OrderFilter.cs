using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using store_accounting_system.Core.Interfaces;

namespace store_accounting_system.Core.Entities.Filters;

public class OrderFilter : IFilter<Order>
{
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
            (!CustomerId.HasValue || o.CustomerId == CustomerId.Value) &&
            (!DateFrom.HasValue || o.Date >= DateFrom.Value) &&
            (!DateTo.HasValue || o.Date <= DateTo.Value) &&
            (!MinTotal.HasValue || o.TotalAmount >= MinTotal.Value) &&
            (!MaxTotal.HasValue || o.TotalAmount <= MaxTotal.Value);
    }

    public override Func<IQueryable<Order>, IOrderedQueryable<Order>>? GetOrderBy()
    {
        return SortField.ToLower() switch
        {
            "TotalAmount" => Increasing
                ? q => q.OrderBy(p => p.TotalAmount)
                : q => q.OrderByDescending(p => p.TotalAmount),
            _ => Increasing
                ? q => q.OrderBy(p => p.Date)
                : q => q.OrderByDescending(p => p.Date)
        };
    }
}