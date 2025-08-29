using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Domain;
using OrderManagementApp.Infrastructure;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Commands
{
    public class PlaceOrderHandler : ICommandHandler<PlaceOrder>
    {
        private readonly AppDbContext _db;
        public PlaceOrderHandler(AppDbContext db) => _db = db;
        public async Task HandleAsync(PlaceOrder command, CancellationToken ct = default)
        {
            var order = await _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == command.OrderId, ct);
            if (order == null) throw new InvalidOperationException("Order not found");
            if (order.Status != OrderStatus.Draft) throw new InvalidOperationException("Only Draft orders can be placed");
            if (!order.Items.Any()) throw new InvalidOperationException("Cannot place an empty order");


            order.Status = OrderStatus.Placed;
            order.TotalAmount = order.Items.Sum(i => i.LineTotal);
            await _db.SaveChangesAsync(ct);
        }
    }
}
