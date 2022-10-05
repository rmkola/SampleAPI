using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Application.Features.Commands.Basket.AddItemToBasket;
using SampleAPI.Application.Features.Commands.Basket.RemoveBasketItem;
using SampleAPI.Application.Features.Commands.Basket.UpdateQuantity;
using SampleAPI.Application.Features.Queries.Basket.GetBasketItems;

namespace SampleAPI.API.Controllers
{
    /// <summary>
    /// Sepet işlemlerini yapan APIdir..
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketsController : ControllerBase
    {
        readonly IMediator _mediator;
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="mediator"></param>
        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Geçerli sepetteki ürünleri liste olarak döner.
        /// </summary>
        /// <param name="getBasketItemsQueryRequest">getBasketItemsQueryRequest türünden parametre gerektirir.</param>
        /// <returns>Liste olarak GetBasketItemsQueryResponse türünden veriler döner.</returns>
        [HttpGet]
        public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest getBasketItemsQueryRequest)
        {
            List<GetBasketItemsQueryResponse> response = await _mediator.Send(getBasketItemsQueryRequest);
            return Ok(response);
        }

        /// <summary>
        /// Geçerli sepete ürün eklemek için kullanılan metoddur.
        /// </summary>
        /// <param name="addItemToBasketCommandRequest">addItemToBasketCommandRequest türünden parametre gerektirir.</param>
        /// <returns>AddItemToBasketCommandResponse türünden veri döner.</returns>
        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Geçerli sepetteki ürünün miktarını değiştirmek için kullanılan metoddur.
        /// </summary>
        /// <param name="updateQuantityCommandRequest">updateQuantityCommandRequest türünden parametre gerektirir.</param>
        /// <returns>UpdateQuantityCommandResponse türünde veri döner.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
            UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Sepetten bir ürünü silmek için kullanılan API metodudur.
        /// </summary>
        /// <param name="removeBasketItemCommandRequest"></param>
        /// <returns></returns>
        [HttpDelete("{BasketItemId}")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
        {
            RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommandRequest);
            return Ok(response);
        }
    }
}
