namespace OrderService.Dtos.Order
{
    public record CreateOrderRequestDto
    {
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}