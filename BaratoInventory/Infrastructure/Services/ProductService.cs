using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Repositories;

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

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductModel> CreateProduct(ProductModel productModel)
        {
            var product = await _productRepository.CreateProduct(productModel);
            return product;
        }

        public async Task DeleteProduct(Guid id)
        {
            await _productRepository.DeleteProduct(id);
        }

        public Task<ProductModel> GetProduct(Guid id)
        {
            return _productRepository.GetProduct(id);
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public Task<bool> ProductModelExists(Guid id)
        {
            return _productRepository.ProductModelExists(id);
        }

        public async Task UpdateProduct(ProductModel productModel)
        {
            await _productRepository.UpdateProduct(productModel);
        }
    }
}
