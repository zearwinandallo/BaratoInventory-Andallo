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
        Task<ProductModel> GetProduct(int id);
        Task UpdateProduct(ProductModel productModel);
        Task<ProductModel> CreateProduct(ProductModel productModel);
        Task<bool> ProductModelExists(int id);
        Task DeleteProduct(int id);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<ProductModel> CreateProduct(ProductModel productModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public Task<bool> ProductModelExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(ProductModel productModel)
        {
            throw new NotImplementedException();
        }
    }
}
