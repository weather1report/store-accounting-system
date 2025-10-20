using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using store_accounting_system.core.Interfaces;

namespace store_accounting_system.core.Entities.Filters;

public class CustomerFilter : IFilter<Customer>
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public DateTime? RegisteredAfter { get; set; }
    public DateTime? RegisteredBefore { get; set; }

    public override Expression<Func<Customer, bool>>? GetExpression()
    {
        return c =>
            (!Id.HasValue || c.Id == Id.Value) &&
            (string.IsNullOrEmpty(Name) || EF.Functions.Like(c.Name!, $"%{Name}%")) &&
            (string.IsNullOrEmpty(Phone) || EF.Functions.Like(c.PhoneNumber!, $"%{Phone}%")) &&
            (!RegisteredAfter.HasValue || c.RegisterDate >= RegisteredAfter.Value) &&
            (!RegisteredBefore.HasValue || c.RegisterDate <= RegisteredBefore.Value);
    }
}