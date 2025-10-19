using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities.Filters;

public class SupplyFilter : IFilter<Supply>
{
    public int? ProductId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public decimal? MinQuantity { get; set; }
    public decimal? MaxQuantity { get; set; }
    
    public string SortField { get; set; } = "Date";
    public bool Increasing { get; set; } = true;

    public override Expression<Func<Supply, bool>>? GetExpression()
    {
        return s =>
            (!ProductId.HasValue || s.ProductId == ProductId.Value) &&
            (!DateFrom.HasValue || s.Date >= DateFrom.Value) &&
            (!DateTo.HasValue || s.Date <= DateTo.Value) &&
            (!MinQuantity.HasValue || s.Quantity >= MinQuantity.Value) &&
            (!MaxQuantity.HasValue || s.Quantity <= MaxQuantity.Value);
    }

    public override Func<IQueryable<Supply>, IOrderedQueryable<Supply>>? GetOrderBy()
    {
        return SortField.ToLower() switch
        {
            "ProductId" => Increasing
                ? q => q.OrderBy(p => p.ProductId)
                : q => q.OrderByDescending(p => p.ProductId),
            "Quantity" => Increasing
                ? q => q.OrderBy(p => p.Quantity)
                : q => q.OrderByDescending(p => p.Quantity),
            _ => Increasing
                ? q => q.OrderBy(p => p.Date)
                : q => q.OrderByDescending(p => p.Date)
        };
    }
}