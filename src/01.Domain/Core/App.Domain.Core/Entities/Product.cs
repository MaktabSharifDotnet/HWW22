using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<CartProduct> CartProducts { get; set; } = [];
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsDeleted { get; set; }
      

    }
}
