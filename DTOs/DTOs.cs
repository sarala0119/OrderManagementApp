namespace OrderManagementApp.DTOs
{
    public record CustomerDto(Guid Id, string Name, string? Email, DateTime CreatedAt);
    public record ProductDto(Guid Id, string Sku, string Name, decimal UnitPrice, DateTime CreatedAt);
    public record OrderItemDto(Guid ProductId, int Quantity, decimal UnitPriceSnapshot, decimal LineTotal);
    public record OrderDto(Guid Id, Guid CustomerId, string Status, DateTime CreatedAt, decimal TotalAmount, List<OrderItemDto> Items);
}
