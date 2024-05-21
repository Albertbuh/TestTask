using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {

        readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Возвращать самый новый заказ, в котором больше одного предмета.
        /// </summary>
        public async Task<Order> GetOrder()
        {
            var order = await _context.Orders
                                    .Include(o => o.User)
                                    .Where(o => o.Quantity > 1)
                                    .OrderByDescending(o => o.CreatedAt)
                                    .FirstOrDefaultAsync();
            if (order == null)
                throw new InvalidOperationException("No order with more than one product");
            //_context.Entry(order).Reference(order => order.User).Load();
            return order;
        }


        /// <summary>
        /// Возвращать заказы от активных пользователей, отсортированные по дате создания.
        /// </summary>
        public async Task<List<Order>> GetOrders()
        {
            var orders = await _context.Orders
                                      .Include(o => o.User)
                                      .Where(o => o.User.Status == Enums.UserStatus.Active)
                                      .OrderBy(o => o.CreatedAt)
                                      .ToListAsync();
            if (orders == null)
                throw new InvalidOperationException("No orders from active users");
            return orders;
        }
    }
}
