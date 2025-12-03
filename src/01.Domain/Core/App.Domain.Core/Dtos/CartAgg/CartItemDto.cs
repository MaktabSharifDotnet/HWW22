namespace App.Domain.Core.Dtos.CartAgg
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; } 
        public int Count { get; set; }
        public decimal TotalPrice => Price * Count;
    }
}