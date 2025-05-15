using TestAssignment.Entity.Models;

namespace TestAssignment.Repository.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<List<Order>> GetOrdersAsync(int userId);
}
