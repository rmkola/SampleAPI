using SampleAPI.Application.Abstractions.Services;
using SampleAPI.Application.DTOs.Order;
using SampleAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SampleAPI.Persistence.Services
{

    /// <summary>
    /// >Siparişlerin yönetildiği servistir.
    /// </summary>
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        /// <summary>
        /// Sipariş oluşturmada kullanıldır. CreateOrder türünden parametre gerektirir.
        /// </summary>
        /// <param name="createOrder"></param>
        /// <returns></returns>
        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);

            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId ?? ""),
                Description = createOrder.Description,
                OrderCode = orderCode
            });
            await _orderWriteRepository.SaveAsync();
        }
        /// <summary>
        /// Siparişi veritabanından silmek için kullanılır.
        /// </summary>
        /// <param name="deleteOrder"></param>
        public void DeleteOrder(DeleteOrder deleteOrder)
        {
             var order = _orderReadRepository.GetWhere(a => a.Id == Guid.Parse(deleteOrder.Id)).FirstOrDefault();
            if (order != null)
                _orderWriteRepository.Remove(order);
        }
        /// <summary>
        /// Sayfalama yönetmi ile belirli bir sayfadaki siparişlerin listesi alınır.
        /// </summary>
        /// <param name="page">Kaçıncı sayfa</param>
        /// <param name="size">Sayfa uzunluğu</param>
        /// <returns></returns>

        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                      .ThenInclude(b => b.User)
                      .Include(o => o.Basket)
                         .ThenInclude(b => b.BasketItems)
                         .ThenInclude(bi => bi.Product);

            var data = query.Skip(page * size).Take(size);
            /*.Take((page * size)..size);*/

            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName
                }).ToListAsync()
            };
        }
        /// <summary>
        /// Verilen parametredeki id değerine sahip siparişi döndürür.
        /// </summary>
        /// <param name="id">Sipariş Idsidir.</param>
        /// <returns>SingleOrder türünden değer döner.</returns>
        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data = await _orderReadRepository.Table
                                 .Include(o => o.Basket)
                                     .ThenInclude(b => b.BasketItems)
                                         .ThenInclude(bi => bi.Product)
                                                 .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new()
            {
                Id = data.Id.ToString(),
                BasketItems = data.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address = data.Address,
                CreatedDate = data.CreatedDate,
                Description = data.Description,
                OrderCode = data.OrderCode
            };
        }

        /// <summary>
        /// Sipariş bilgisinin güncellenmesinde kullanılır.
        /// </summary>
        /// <param name="updateOrder"></param>
        /// <returns></returns>
        public async Task UpdateOrderAsync(UpdateOrder updateOrder)
        {
           

            var order = _orderReadRepository.GetWhere(a => a.Id == Guid.Parse(updateOrder.BasketId)).FirstOrDefault();
            if (order != null)
            {
                order.Address = updateOrder.Address;
                order.Description = updateOrder.Description;
                _orderWriteRepository.Update(order);
                await _orderWriteRepository.SaveAsync();
            }
             
        }
    }
}
