namespace DeliveryService.Application.Dtos
{
    public record DeliveryRequestDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; } 
        public required string Status { get; set; }
    }
}