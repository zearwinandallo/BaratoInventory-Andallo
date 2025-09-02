using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public static class SeederExtensions
    {
        public static IHost EnsureDbCreatedAndSeed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var db = services.GetRequiredService<AppDbContext>();

            db.Database.EnsureCreated();

            if (!db.Products.Any())
            {
                var now = DateTime.UtcNow;
                db.Products.AddRange(
                    new ProductModel { Name = "Smartphone", Category = ProductCategoryEnum.Electronics, Price = 24999.00, Quantity = 25, CreatedAt = now },  // ₱24,999
                    new ProductModel { Name = "T-Shirt", Category = ProductCategoryEnum.Clothing, Price = 399.00, Quantity = 100, CreatedAt = now },        // ₱399
                    new ProductModel { Name = "Coffee Beans", Category = ProductCategoryEnum.Food, Price = 299.00, Quantity = 50, CreatedAt = now },        // ₱299
                    new ProductModel { Name = "Office Chair", Category = ProductCategoryEnum.Furniture, Price = 4999.00, Quantity = 10, CreatedAt = now },  // ₱4,999
                    new ProductModel { Name = "Novel Book", Category = ProductCategoryEnum.Books, Price = 499.00, Quantity = 40, CreatedAt = now },         // ₱499
                    new ProductModel { Name = "Gift Card", Category = ProductCategoryEnum.Other, Price = 1000.00, Quantity = 75, CreatedAt = now }          // ₱1,000
                );
                db.SaveChanges();
            }

            return host;
        }
    }
}
