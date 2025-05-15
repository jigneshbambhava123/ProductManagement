using TestAssignment.Entity.Helper;
using TestAssignment.Entity.Models;

namespace TestAssignment.Service.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task AddProductAsync(Product product);
    Task<Product> GetProductByIdAsync(int id);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<PaginatedList<Product>> GetPagedProductsAsync(int page, int pageSize, string search, string time, string sortColumn, string sortDirection, DateTime? fromDate, DateTime? toDate);

}
