using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAssignment.Entity.Data;
using TestAssignment.Entity.Models;
using TestAssignment.Entity.ViewModels;
using TestAssignment.Service.Interfaces;

namespace TestAssignment.Web.Controllers;

public class OrderController: Controller
{
    private readonly TestAssignmentContext _context;
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;

    public OrderController(TestAssignmentContext context,IProductService productService,IOrderService orderService)
    {
        _context = context;
        _productService = productService;
        _orderService = orderService;
        
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> CreateOrder()
    {
        return View(await _productService.GetAllProductsAsync());
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrder(SaveOrderRequest request)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userid))
            {
                ModelState.AddModelError("id", "Invalid User ID.");
                return BadRequest(ModelState);
            }
            var result =await _orderService.SaveOrder(request,userid);

            if (result.success)
            {
                return Json(new { success = true, message = result.message});            }
            else
            {
                return Json(new { success = false, message = result.message});
            }

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving order: {ex}");
            return Json(new { success = false, message = "Failed to save order: " + ex.Message });
        }
    }

    [Authorize(Roles = "Admin,User")]
    public IActionResult Order()
    {
        return View();
    }

    public async Task<IActionResult> OrderList()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userid))
        {
            ModelState.AddModelError("id", "Invalid User ID.");
            return BadRequest(ModelState);
        }
        var pagedOrders = await _orderService.GetOrdersAsync(userid);
        return PartialView("_OrderList",pagedOrders);
    }

    // GET: Order/Edit/5
    public async Task<IActionResult> EditStatus(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _orderService.GetOrderByIdAsync(id??0);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

    // POST: Product/Edit/5
    [HttpPost]
    public async Task<IActionResult> EditStatus(string status,int orderId)
    {
        try
        {
            await _orderService.UpdateOrderAsync(orderId,status);
        }
        catch (Exception ex)
        {
            return View();
        }
        return RedirectToAction("Order","Order");
    }

}
