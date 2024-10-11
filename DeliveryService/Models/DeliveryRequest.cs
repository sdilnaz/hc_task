namespace DeliveryService.Models
{
    public class DeliveryRequest
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Создана";
    }
}