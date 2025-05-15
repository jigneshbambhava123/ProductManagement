using Microsoft.EntityFrameworkCore;
using TestAssignment.Entity.Data;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Interfaces;

namespace TestAssignment.Repository.Implementations;

public class OrderRepository: GenericRepository<Order>, IOrderRepository
{
    
    public OrderRepository(TestAssignmentContext context) : base(context)
    {
    }

    public async Task<List<Order>> GetOrdersAsync(int userId)
    {
        var query = _dbSet.AsQueryable();

        // Apply status filter
        if (userId!=2)
        {

             query = query.Where(t => t.Userid == userId);
            
        }

        var items = await query.Where(t=> !t.Isdeleted)
                                .ToListAsync();

        return new List<Order>(items);
    }
}
