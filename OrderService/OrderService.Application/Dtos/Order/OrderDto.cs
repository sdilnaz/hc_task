namespace OrderService.Application.Dtos.Order
{
    public record OrderDto
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}