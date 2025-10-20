using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities.Filters;

public class ProductFilter : IFilter<Product>
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    
    public bool Increasing { get; set; } = true;

    public override Expression<Func<Product, bool>>? GetExpression()
    {
        return p =>
            (!Id.HasValue || p.Id == Id.Value) &&
            (string.IsNullOrEmpty(Name) || EF.Functions.Like(p.Name!, $"%{Name}%")) &&
            (!MinPrice.HasValue || p.Price >= MinPrice.Value) &&
            (!MaxPrice.HasValue || p.Price <= MaxPrice.Value) &&
            (!MinQuantity.HasValue || p.Quantity >= MinQuantity.Value) &&
            (!MaxQuantity.HasValue || p.Quantity <= MaxQuantity.Value);
    }
}