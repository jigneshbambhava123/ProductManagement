using TestAssignment.Entity.Models;
using TestAssignment.Entity.ViewModels;

namespace TestAssignment.Service.Interfaces;

public interface IOrderService
{
    Task<Order> GetOrderByIdAsync(int id);
    Task<(bool success, string message)> SaveOrder(SaveOrderRequest request,int userId);
    Task<List<Order>> GetOrdersAsync(int userId);
    Task UpdateOrderAsync(int orderId,string status);
}
