using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BaratoInventory.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(
        IProductService productService
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseModel>> GetProducts()
        {
            var products = await productService.GetProducts();
            return Ok(new ResponseModel { Success = true, Data = products });
        }
    }
}
