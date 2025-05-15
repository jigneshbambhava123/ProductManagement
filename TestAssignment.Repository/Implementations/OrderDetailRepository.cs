using TestAssignment.Entity.Data;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Interfaces;

namespace TestAssignment.Repository.Implementations;

public class OrderDetailRepository : GenericRepository<Orderdetail>, IOrderDetailRepository
{
    public OrderDetailRepository(TestAssignmentContext context) : base(context)
    {
    }
}
