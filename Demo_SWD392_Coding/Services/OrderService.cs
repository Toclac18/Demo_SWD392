using System.Collections.Generic;
using Demo_SWD392_Coding.Models;
using Demo_SWD392_Coding.Repository.IRepository;

namespace Demo_SWD392_Coding.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public Order PlaceOrder(Order order)
        {
            return _orderRepository.AddOrder(order);
        }
    }
}
