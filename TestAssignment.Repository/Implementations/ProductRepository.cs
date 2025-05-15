using Microsoft.EntityFrameworkCore;
using TestAssignment.Entity.Data;
using TestAssignment.Entity.Helper;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Interfaces;

namespace TestAssignment.Repository.Implementations;

public class ProductRepository  : GenericRepository<Product> , IProductRepository
{
    public ProductRepository(TestAssignmentContext context):base(context)
    {
        
    }

    public async Task<PaginatedList<Product>> GetPagedProductsAsync(int page, int pageSize, string search, string time, string sortColumn, string sortDirection, DateTime? fromDate, DateTime? toDate)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();
            query = query.Where(t => t.Name.ToLower().Contains(search));
        }

       
        var totalItems = await query.Where(t=> !t.Isdeleted).CountAsync();
        var items = await query.Where(t=> !t.Isdeleted).Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .Select(t => new Product
                                {   
                                    Id = t.Id,
                                    Name = t.Name,
                                    Rate = t.Rate,
                                    Category = t.Category,
                                    Quantity = t.Quantity
                                })
                                .ToListAsync();

        return new PaginatedList<Product>(items, totalItems, page, pageSize);
    }
}
