using Demo_SWD392_Coding.Models;

namespace Demo_SWD392_Coding.Repository.IRepository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Order AddOrder(Order order);
    }
}
