using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Domain;
using OrderManagementApp.Infrastructure;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Commands
{
    public class PayOrderHandler : ICommandHandler<PayOrder>
    {
        private readonly AppDbContext _db;
        public PayOrderHandler(AppDbContext db) => _db = db;
        public async Task HandleAsync(PayOrder command, CancellationToken ct = default)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == command.OrderId, ct);
            if (order == null) throw new InvalidOperationException("Order not found");
            if (order.Status != OrderStatus.Placed) throw new InvalidOperationException("Only placed orders can be paid");
            order.Status = OrderStatus.Paid;
            await _db.SaveChangesAsync(ct);
        }
    }
}
