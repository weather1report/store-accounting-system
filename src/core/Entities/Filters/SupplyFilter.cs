using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities.Filters;

public class SupplyFilter : IFilter<Supply>
{
    public int? Id { get; set; }
    public int? ProductId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    public override Expression<Func<Supply, bool>>? GetExpression()
    {
        return s =>
            (!Id.HasValue || s.Id == Id.Value) &&
            (!ProductId.HasValue || s.ProductId == ProductId.Value) &&
            (!DateFrom.HasValue || s.Date >= DateFrom.Value) &&
            (!DateTo.HasValue || s.Date <= DateTo.Value);
    }
}