using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetProducts();
        Task<ProductModel> GetProduct(int id);
        Task UpdateProduct(ProductModel productModel);
        Task<ProductModel> CreateProduct(ProductModel productModel);
        Task<bool> ProductModelExists(int id);
        Task DeleteProduct(int id);
    }
    public class ProductRepository(AppDbContext dbContext) : IProductRepository
    {
        public Task<List<ProductModel>> GetProducts()
        {
            return dbContext.Products.ToListAsync();
        }

        public async Task<ProductModel> GetProduct(int id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);
            if (product == null)
                throw new KeyNotFoundException($"Product with Id {id} not found.");

            return product;
        }

        public async Task<ProductModel> CreateProduct(ProductModel productModel)
        {
            dbContext.Products.Add(productModel);
            await dbContext.SaveChangesAsync();
            return productModel;
        }
        public async Task UpdateProduct(ProductModel productModel)
        {
            dbContext.Entry(productModel).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
        public Task<bool> ProductModelExists(int id)
        {
            return dbContext.Products.AnyAsync(e => e.Id == id);
        }
        public async Task DeleteProduct(int id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
