namespace task1.Dtos.Order
{
    public record UpdateOrderRequestDto
    {
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}