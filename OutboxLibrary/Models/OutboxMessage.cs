namespace OutboxLibrary.Models
{
    public class OutboxMessage
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty; 
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }
        public bool Processed { get; set; } = false; 
    }
}