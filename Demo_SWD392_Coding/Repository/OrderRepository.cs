using System.Collections.Generic;
using System.Linq;
using Demo_SWD392_Coding.Models;
using Demo_SWD392_Coding.Repository.IRepository;

namespace Demo_SWD392_Coding.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HospitalDbContext _context;

        public OrderRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
    }
}
