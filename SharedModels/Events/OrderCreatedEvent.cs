namespace SharedModels.Events
{
    public record OrderCreatedEvent
    (
        int Id,
        string ProductName,
        int Quantity,
        decimal Price
    );
}