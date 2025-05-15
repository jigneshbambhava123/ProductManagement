using Microsoft.AspNetCore.Mvc;
using TestAssignment.Entity.Data;
using TestAssignment.Entity.Models;
using TestAssignment.Service.Interfaces;

namespace TestAssignment.Web.Controllers;

public class ProductController: Controller
{
    private readonly TestAssignmentContext _context;
    private readonly IProductService _productService;

    public ProductController(TestAssignmentContext context,IProductService productService)
    {
        _context = context;
        _productService = productService;
        
    }

    // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllProductsAsync());
        }

         public async Task<IActionResult> ProductList(int page = 1, int pageSize = 5, string search = "", string time="", string sortColumn = "Id", string sortDirection = "asc", string fromDate = "", string toDate = "")
        {
            DateTime? startDate = string.IsNullOrEmpty(fromDate) ? (DateTime?)null : DateTime.Parse(fromDate);
            DateTime? endDate = string.IsNullOrEmpty(toDate) ? (DateTime?)null : DateTime.Parse(toDate);
            var pagedOrders = await _productService.GetPagedProductsAsync(page, pageSize, search, time, sortColumn, sortDirection, startDate, endDate);
             return PartialView("_ProductList",pagedOrders);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);
                
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id??0);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateProductAsync(product);
                }
                catch (Exception ex)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var product = await _productService.GetProductByIdAsync(id??0);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
}

