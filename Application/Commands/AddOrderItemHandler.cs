using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Domain;
using OrderManagementApp.Infrastructure;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Commands
{
    public class AddOrderItemHandler : ICommandHandler<AddOrderItem>
    {
        private readonly AppDbContext _db;
        public AddOrderItemHandler(AppDbContext db) => _db = db;
        public async Task HandleAsync(AddOrderItem command, CancellationToken ct = default)
        {
            var order = await _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == command.OrderId, ct);
            if (order == null) throw new InvalidOperationException("Order not found");
            if (order.Status != OrderStatus.Draft) throw new InvalidOperationException("Can only add items to Draft orders");


            var product = await _db.Products.FindAsync(new object[] { command.ProductId }, ct);
            if (product == null) throw new InvalidOperationException("Product not found");


            var item = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = product.Id,
                Quantity = command.Quantity,
                UnitPriceSnapshot = product.UnitPrice,
                LineTotal = product.UnitPrice * command.Quantity
            };
            order.Items.Add(item);
            order.TotalAmount = order.Items.Sum(i => i.LineTotal);
            await _db.SaveChangesAsync(ct);
        }
    }
}
