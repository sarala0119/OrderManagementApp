using Microsoft.EntityFrameworkCore;
using OrderManagementApp.DTOs;
using OrderManagementApp.Infrastructure;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Queries
{
    public class GetOrderByIdHandler : IQueryHandler<GetOrderById, OrderDto>
    {
        private readonly AppDbContext _db;
        public GetOrderByIdHandler(AppDbContext db) => _db = db;
        public async Task<OrderDto> HandleAsync(GetOrderById query, CancellationToken ct = default)
        {
            var o = await _db.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == query.Id, ct);
            if (o == null) throw new KeyNotFoundException("Order not found");
            var items = o.Items.Select(i => new OrderItemDto(i.ProductId, i.Quantity, i.UnitPriceSnapshot, i.LineTotal)).ToList();
            return new OrderDto(o.Id, o.CustomerId, o.Status.ToString(), o.CreatedAt, o.TotalAmount, items);
        }
    }
}
