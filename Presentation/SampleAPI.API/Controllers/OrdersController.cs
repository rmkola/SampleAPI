using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Application.Features.Commands.Order.CreateOrder;
using SampleAPI.Application.Features.Commands.Order.DeleteOrder;
using SampleAPI.Application.Features.Commands.Order.UpdateOrder;
using SampleAPI.Application.Features.Queries.Order.GetAllOrders;
using SampleAPI.Application.Features.Queries.Order.GetOrderById;

namespace SampleAPI.API.Controllers
{
    /// <summary>
    /// Siparişler için hazırlanan APIdir.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediator;
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="mediator"></param>
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Verilen ID deki sipariş bilgilerini döner.
        /// </summary>
        /// <param name="getOrderByIdQueryRequest">getOrderByIdQueryRequest türünden parametre gerektirir.</param>
        /// <returns>GetOrderByIdQueryResponse türünde veri döner.</returns>
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest getOrderByIdQueryRequest)
        {
            GetOrderByIdQueryResponse response = await _mediator.Send(getOrderByIdQueryRequest);
            return Ok(response);
        }

        /// <summary>
        /// Geçerli kullanıcının tüm siparişlerini döner.
        /// </summary>
        /// <param name="getAllOrdersQueryRequest">getAllOrdersQueryRequest türünden parametre gerektirir.</param>
        /// <returns>GetAllOrdersQueryResponse türünden veri döner.</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest)
        {
            GetAllOrdersQueryResponse response = await _mediator.Send(getAllOrdersQueryRequest);
            return Ok(response);
        }

        /// <summary>
        /// Geçerli kullanıcı için sipariş oluşturmaya yarayan metoddur.
        /// </summary>
        /// <param name="createOrderCommandRequest">createOrderCommandRequest türünden parametre gerektirir.</param>
        /// <returns>CreateOrderCommandResponse türünden veri döner.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Geçerli kullanıcının verilen siparişini günceller.
        /// </summary>
        /// <param name="UpdateOrderCommandRequest">UpdateOrderCommandRequest türünden parametre gerektirir.</param>
        /// <returns>UpdateOrderCommandResponse türünden veri döner.</returns>
        [HttpPut]
        public async Task<ActionResult> UpdateOrder(UpdateOrderCommandRequest UpdateOrderCommandRequest)
        {
            UpdateOrderCommandResponse response = await _mediator.Send(UpdateOrderCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Geçerli kullanıcının ID si verilen siparişi siler.
        /// </summary>
        /// <param name="deleteOrderCommandRequest">deleteOrderCommandRequest türünden parametre gerektirir.</param>
        /// <returns>DeleteOrderCommandResponse türünden veri döner.</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(DeleteOrderCommandRequest deleteOrderCommandRequest)
        {
            DeleteOrderCommandResponse response = await _mediator.Send(deleteOrderCommandRequest);
            return Ok(response);
        }
    }
}
