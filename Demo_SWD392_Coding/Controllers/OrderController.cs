using Demo_SWD392_Coding.Models;
using Demo_SWD392_Coding.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo_SWD392_Coding.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult PlaceOrder()
        {
            return View(new Order()); // Ensure model is passed
        }

        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            _orderService.PlaceOrder(order);
            TempData["SuccessMessage"] = "Order placed successfully!";

            return RedirectToAction("OrderList");
        }

        public IActionResult OrderList()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }
    }
}
