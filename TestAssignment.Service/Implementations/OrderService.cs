using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Interfaces;
using TestAssignment.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAssignment.Entity.ViewModels;


namespace TestAssignment.Service.Implementations;

public class OrderService: IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    public OrderService(IOrderRepository orderRepository,IOrderDetailRepository orderDetailRepository)
    {
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
    }

     public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task UpdateOrderAsync(int orderId,string status)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        order.Status = status;
        await _orderRepository.UpdateAsync(order);
    }

    public async Task<(bool success, string message)> SaveOrder(SaveOrderRequest request,int userId)
        {
            using (var transaction = await _orderRepository.BeginTransactionAsync())
            {
                try
                {
                    // Create new order
                    var order = new Order
                    {
                        Orderdate = DateTime.Now,
                        Userid = userId,
                        Status = "Pending",
                        Paymentmode = "Cash", // Default
                        Orderamount = request.TotalAmount, // Initial
                        Subtotal = 0,
                        Paidamount = 0,
                        Rating = 5,
                        Isdeleted = false,
                        Updatedat = DateTime.Now,
                    };

                    await _orderRepository.AddAsync(order);

                    // Get the generated OrderId
                    int orderId = order.Id;

                    if(request.Items != null){
                        foreach (var itemData in request.Items)
                        {
                            var orderDetail = new Orderdetail
                            {
                                Orderid = orderId,
                                Productid = itemData.ItemId,
                                Quantity = itemData.Quantity,
                                Unitprice = itemData.ItemPrice,
                                Productamount = itemData.ItemPrice * itemData.Quantity,
                                Isdeleted = false,
                                Updatedat = DateTime.Now,
                                Createdat = DateTime.Now,
                                Isprepared = 0
                            };

                                await _orderDetailRepository.AddAsync(orderDetail);
                                await _orderDetailRepository.SaveChangesAsync(); 
                                
                            
                        }
                        await _orderDetailRepository.SaveChangesAsync();
                    }else{
                        return (false, "Please select atleast 1 Product");

                    }

                    // Commit the transaction
                    await transaction.CommitAsync();

                    // Pass the ViewModel to the view
                    return ( true, "Order Created successfully");

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.Error.WriteLine($"Error saving order: {ex}");
                    throw; 
                }
            }
        }

    public async Task<List<Order>> GetOrdersAsync(int userId)
    {
        return await _orderRepository.GetOrdersAsync(userId);
    }
}
