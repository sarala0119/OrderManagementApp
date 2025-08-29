using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Queries
{
    public record GetOrderById(Guid Id) : IQuery<OrderManagementApp.DTOs.OrderDto>;
    public record GetCustomerById(Guid Id) : IQuery<OrderManagementApp.DTOs.CustomerDto>;
    public record GetProductBySku(string Sku) : IQuery<OrderManagementApp.DTOs.ProductDto>;
}
