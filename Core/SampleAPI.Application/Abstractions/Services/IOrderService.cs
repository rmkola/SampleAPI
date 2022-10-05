using SampleAPI.Application.DTOs.Order;

namespace SampleAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrder createOrder);
        void DeleteOrder(DeleteOrder createOrder);
        Task UpdateOrderAsync(UpdateOrder createOrder);
        Task<ListOrder> GetAllOrdersAsync(int page, int size);
        Task<SingleOrder> GetOrderByIdAsync(string id);
    }
}
