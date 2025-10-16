using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using store_accounting_system.Core.Interfaces;

namespace store_accounting_system.Core.Entities.Filters;

public class ProductFilter : IFilter<Product>
{
    public string? Name { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }

    public string SortField { get; set; } = "Name";
    public bool Increasing { get; set; } = true;

    public override Expression<Func<Product, bool>>? GetExpression()
    {
        return p =>
            (string.IsNullOrEmpty(Name) || EF.Functions.Like(p.Name!, $"%{Name}%")) &&
            (!MinPrice.HasValue || p.Price >= MinPrice.Value) &&
            (!MaxPrice.HasValue || p.Price <= MaxPrice.Value) &&
            (!MinQuantity.HasValue || p.Quantity >= MinQuantity.Value) &&
            (!MaxQuantity.HasValue || p.Quantity <= MaxQuantity.Value);
    }

    public override Func<IQueryable<Product>, IOrderedQueryable<Product>>? GetOrderBy()
    {
        return SortField.ToLower() switch
        {
            "price" => Increasing
                ? q => q.OrderBy(p => p.Price)
                : q => q.OrderByDescending(p => p.Price),
            "quantity" => Increasing
                ? q => q.OrderBy(p => p.Quantity)
                : q => q.OrderByDescending(p => p.Quantity),
            _ => Increasing
                ? q => q.OrderBy(p => p.Name)
                : q => q.OrderByDescending(p => p.Name)
        };
    }
}