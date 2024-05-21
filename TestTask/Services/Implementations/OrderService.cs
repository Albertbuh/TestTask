using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        public Task<Order> GetOrder()
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}
