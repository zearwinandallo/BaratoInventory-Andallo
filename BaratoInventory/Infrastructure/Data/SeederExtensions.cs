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
                    new ProductModel { Name = "Smartphone", Category = ProductCategoryEnum.Electronics, Price = 24999.00, Quantity = 25, LastUpdated = now },  
                    new ProductModel { Name = "T-Shirt", Category = ProductCategoryEnum.Clothing, Price = 399.00, Quantity = 100, LastUpdated = now },        
                    new ProductModel { Name = "Coffee Beans", Category = ProductCategoryEnum.Food, Price = 299.00, Quantity = 50, LastUpdated = now },        
                    new ProductModel { Name = "Office Chair", Category = ProductCategoryEnum.Furniture, Price = 4999.00, Quantity = 10, LastUpdated = now },  
                    new ProductModel { Name = "Novel Book", Category = ProductCategoryEnum.Books, Price = 499.00, Quantity = 40, LastUpdated = now },         
                    new ProductModel { Name = "Gift Card", Category = ProductCategoryEnum.Other, Price = 1000.00, Quantity = 75, LastUpdated = now }          
                );
                db.SaveChanges();
            }

            return host;
        }
    }
}
