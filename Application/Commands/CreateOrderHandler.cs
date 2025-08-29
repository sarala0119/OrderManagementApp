using OrderManagementApp.Domain;
using OrderManagementApp.Infrastructure;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Commands
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly AppDbContext _db;
        public CreateOrderHandler(AppDbContext db) => _db = db;
        public async Task HandleAsync(CreateOrder command, CancellationToken ct = default)
        {
            var customer = await _db.Customers.FindAsync(new object[] { command.CustomerId }, ct);
            if (customer == null) throw new InvalidOperationException("Customer not found");
            var order = new Order { Id = Guid.NewGuid(), CustomerId = customer.Id, Status = OrderStatus.Draft, CreatedAt = DateTime.UtcNow, TotalAmount = 0m };
            _db.Orders.Add(order);
            await _db.SaveChangesAsync(ct);
        }
    }
}
