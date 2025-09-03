using Core.Entities;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel>> GetProduct(Guid id)
        {
            var productModel = await productService.GetProduct(id);

            if (productModel == null)
            {
                return Ok(new ResponseModel { Success = false, ErrorMessage = "Not Found" });
            }
            return Ok(new ResponseModel { Success = true, Data = productModel });
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel>> GetProducts()
        {
            var products = await productService.GetProducts();
            return Ok(new ResponseModel { Success = true, Data = products });
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> CreateProduct(ProductModel productModel)
        {
            await productService.CreateProduct(productModel);
            return Ok(new ResponseModel { Success = true });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductModel productModel)
        {
            if (id != productModel.Id || !await productService.ProductModelExists(id))
            {
                return Ok(new ResponseModel { Success = false, ErrorMessage = "Bad request" });
            }

            await productService.UpdateProduct(productModel);
            return Ok(new ResponseModel { Success = true });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (!await productService.ProductModelExists(id))
            {
                return Ok(new ResponseModel { Success = false, ErrorMessage = "Not Found" });
            }
            await productService.DeleteProduct(id);
            return Ok(new ResponseModel { Success = true });
        }
    }
}
