using OrderManagementApp.Domain;
using OrderManagementApp.Infrastructure;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Commands
{
    public class CreateCustomerHandler: ICommandHandler<CreateCustomer>
    {
        private readonly AppDbContext _db;
        public CreateCustomerHandler(AppDbContext db) => _db = db;
        public async Task HandleAsync(CreateCustomer command, CancellationToken ct = default)
        {
            var c = new Customer { Id = Guid.NewGuid(), Name = command.Name, Email = command.Email, CreatedAt = DateTime.UtcNow };
            _db.Customers.Add(c);
            await _db.SaveChangesAsync(ct);
        }
    }
}
