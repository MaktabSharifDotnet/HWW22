using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.Dtos.ProductAgg
{
    public class ProductDto
    {
        public int Id { get; set; }

       
        public string Title { get; set; }


        public string CategoryName { get; set; }

        public string Description { get; set; }


        public string Image { get; set; }


        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Inventory { get; set; }


        public int CategoryId { get; set; }
    }
}