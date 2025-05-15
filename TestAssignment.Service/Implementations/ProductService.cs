using TestAssignment.Entity.Helper;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Interfaces;
using TestAssignment.Service.Interfaces;

namespace TestAssignment.Service.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return _productRepository.GetAllAsync();
    }

    public async Task AddProductAsync(Product product)
    {
        await _productRepository.AddAsync(product);
    }
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

      public async Task<PaginatedList<Product>> GetPagedProductsAsync(int page, int pageSize, string search, string time, string sortColumn, string sortDirection, DateTime? fromDate, DateTime? toDate)
    {
        return await _productRepository.GetPagedProductsAsync(page, pageSize, search, time, sortColumn, sortDirection, fromDate, toDate);
    }
}