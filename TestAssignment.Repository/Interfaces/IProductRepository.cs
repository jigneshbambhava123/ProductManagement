using TestAssignment.Entity.Helper;
using TestAssignment.Entity.Models;

namespace TestAssignment.Repository.Interfaces;

public interface IProductRepository :IGenericRepository<Product> 
{
    Task<PaginatedList<Product>> GetPagedProductsAsync(int page, int pageSize, string search, string time, string sortColumn, string sortDirection, DateTime? fromDate, DateTime? toDate);

}
