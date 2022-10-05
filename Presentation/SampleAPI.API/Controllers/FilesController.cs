using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.API.Controllers
{
    /// <summary>
    /// Dosyalar için hazırlanan APIdir.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        readonly IConfiguration _configuration;
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="configuration"></param>
        public FilesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Veritabanı URL sini döner.
        /// </summary>
        /// <returns>Veritabanı URL'si.</returns>
        [HttpGet("[action]")]
        public IActionResult GetBaseStorageUrl()
        {
            return Ok(new
            {
                Url = _configuration["BaseStorageUrl"]
            });
        }

    }
}
