using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Entities
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ProductCategoryEnum Category { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

    }
}
