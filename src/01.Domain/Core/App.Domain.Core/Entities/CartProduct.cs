namespace App.Domain.Core.Entities
{
    public class CartProduct
    {      
        public int Count { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
      
        public bool IsDeleted { get; set; }
    }
}