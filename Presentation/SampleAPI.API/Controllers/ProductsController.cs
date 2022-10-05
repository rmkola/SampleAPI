using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Application.Features.Commands.Product.CreateProduct;
using SampleAPI.Application.Features.Commands.Product.RemoveProduct;
using SampleAPI.Application.Features.Commands.Product.UpdateProduct;
using SampleAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage;
using SampleAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using SampleAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using SampleAPI.Application.Features.Queries.Product.GetAllProduct;
using SampleAPI.Application.Features.Queries.Product.GetByIdProduct;
using SampleAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using System.Net;

namespace SampleAPI.API.Controllers
{
    /// <summary>
    /// Ürünler için hazırlanan API'dir.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<ProductsController> _logger;
        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Tüm ürünleri listeleyen metoddur.
        /// </summary>
        /// <param name="getAllProductQueryRequest">getAllProductQueryRequest türünden parametre gerektirir.</param>
        /// <returns>GetAllProductQueryResponse türünden veri döner.</returns>

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        /// <summary>
        /// verilen ID deki ürünü getiren metoddur.
        /// </summary>
        /// <param name="getByIdProductQueryRequest">getByIdProductQueryRequest türünden parametre gerektirir.</param>
        /// <returns>GetByIdProductQueryResponse türünden veri döner.</returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

        /// <summary>
        /// Ürünün yalnızca admin şemasına sahip kullanıcılar için oluşturulmasını sağlayan metoddur.
        /// </summary>
        /// <param name="createProductCommandRequest">createProductCommandRequest türünden veri gerektirir.</param>
        /// <returns>CreateProductCommandResponse türünden veri döner.</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }
        /// <summary>
        /// Ürünün yalnızca admin şemasına sahip kullanıcılar için güncellenmesini sağlayan metoddur.
        /// </summary>
        /// <param name="updateProductCommandRequest">updateProductCommandRequest türünden parametre gerektirir.</param>
        /// <returns>UpdateProductCommandResponse türünden veri döner.</returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        /// <summary>
        /// Ürünün yalnızca admin şemasına sahip kullanıcılar tarafından silinmesini sağlayan metoddur.
        /// </summary>
        /// <param name="removeProductCommandRequest">removeProductCommandRequest türünden parametre gerektirir.</param>
        /// <returns>RemoveProductCommandResponse türünden veri döner.</returns>
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }

        /// <summary>
        /// Ürünlere resim eklemek için adminler tarafından kullanılabilen dosya yükleme metodudur.
        /// </summary>
        /// <param name="uploadProductImageCommandRequest">uploadProductImageCommandRequest türünden parametre gerektirir.</param>
        /// <returns>UploadProductImageCommandResponse türünden veri döner.returns>
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        /// <summary>
        /// Ürün resimlerini adminler tarafından getirilebilmesini sağlayan metoddur.
        /// </summary>
        /// <param name="getProductImagesQueryRequest">getProductImagesQueryRequest türünden parametre gerektirir.</param>
        /// <returns>Liste olarak GetProductImagesQueryResponse türünden veriler döner. </returns>
        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }
        /// <summary>
        /// Ürün resimlerinin adminler tarafından silinebilmesini sağlayan metoddur.
        /// </summary>
        /// <param name="removeProductImageCommandRequest">removeProductImageCommandRequest türünden parametre gerektirir.</param>
        /// <param name="imageId">imageId Guid türünden ürün Id sidir. Parametre olarak talep edilir.</param>
        /// <returns>RemoveProductImageCommandResponse türünden veri döner.</returns>
        [HttpDelete("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            //Ders sonrası not !
            //Burada RemoveProductImageCommandRequest sınıfı içerisindeki ImageId property'sini de 'FromQuery' attribute'u ile işaretleyebilirdik!

            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
            return Ok();
        }

        /// <summary>
        /// Ürünün ana resminin adminler tarafından değişitirlebilmesini sağlayan metoddur.
        /// </summary>
        /// <param name="changeShowcaseImageCommandRequest">changeShowcaseImageCommandRequest türünden parametre gerektirir.</param>
        /// <returns>ChangeShowcaseImageCommandResponse türünden veri döner.</returns>
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            ChangeShowcaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);
            return Ok(response);
        }


    }
}
