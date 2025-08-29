using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Commands
{
    public record CreateCustomer(string Name, string? Email) : ICommand;
    public record CreateProduct(string Sku, string Name, decimal UnitPrice) : ICommand;
    public record CreateOrder(Guid CustomerId) : ICommand;
    public record AddOrderItem(Guid OrderId, Guid ProductId, int Quantity) : ICommand;
    public record PlaceOrder(Guid OrderId) : ICommand;
    public record PayOrder(Guid OrderId) : ICommand;
    public record CancelOrder(Guid OrderId) : ICommand;
}
