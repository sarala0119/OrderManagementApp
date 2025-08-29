using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Domain;
using OrderManagementApp.Infrastructure;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Commands
{
    public class CancelOrderHandler : ICommandHandler<CancelOrder>
    {
        private readonly AppDbContext _db;
        public CancelOrderHandler(AppDbContext db) => _db = db;
        public async Task HandleAsync(CancelOrder command, CancellationToken ct = default)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == command.OrderId, ct);
            if (order == null) throw new InvalidOperationException("Order not found");
            if (order.Status == OrderStatus.Paid) throw new InvalidOperationException("Cannot cancel a paid order");
            order.Status = OrderStatus.Canceled;
            await _db.SaveChangesAsync(ct);
        }
    }
}
