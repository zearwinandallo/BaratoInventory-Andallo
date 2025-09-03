using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Core.Services
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetProducts();
        Task<ProductModel> GetProduct(Guid id);
        Task UpdateProduct(ProductModel productModel);
        Task<ProductModel> CreateProduct(ProductModel productModel);
        Task<bool> ProductModelExists(Guid id);
        Task DeleteProduct(Guid id);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDistributedCache _cacheService;

        public ProductService(
            IProductRepository productRepository,
            IDistributedCache cacheService
            )
        {
            _productRepository = productRepository;
            _cacheService = cacheService;
        }
        public async Task<ProductModel> CreateProduct(ProductModel productModel)
        {
            var product = await _productRepository.CreateProduct(productModel);
            await _cacheService.RemoveAsync("list_products");
            return product;
        }

        public async Task DeleteProduct(Guid id)
        {
            await _productRepository.DeleteProduct(id);
            await _cacheService.RemoveAsync("list_products");
        }

        public Task<ProductModel> GetProduct(Guid id)
        {
            return _productRepository.GetProduct(id);
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            var cacheValue = await _cacheService.GetStringAsync("list_products");
            if (!string.IsNullOrEmpty(cacheValue))
            {
                return JsonConvert.DeserializeObject<List<ProductModel>>(cacheValue);
            }

            var products = await _productRepository.GetProducts();
            await _cacheService.SetStringAsync("list_products", JsonConvert.SerializeObject(products));
            return products;
        }

        public Task<bool> ProductModelExists(Guid id)
        {
            return _productRepository.ProductModelExists(id);
        }

        public async Task UpdateProduct(ProductModel productModel)
        {
            await _productRepository.UpdateProduct(productModel);
            await _cacheService.RemoveAsync("list_products");
        }
    }
}
