using OrderManagementApp.Domain;
using OrderManagementApp.Infrastructure;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Commands
{
    public class CreateProductHandler:ICommandHandler<CreateProduct>
    {
        private readonly AppDbContext _db;
        public CreateProductHandler(AppDbContext db) => _db = db;
        public async Task HandleAsync(CreateProduct command, CancellationToken ct = default)
        {
            var p = new Product { Id = Guid.NewGuid(), Sku = command.Sku, Name = command.Name, UnitPrice = command.UnitPrice, CreatedAt = DateTime.UtcNow };
            _db.Products.Add(p);
            await _db.SaveChangesAsync(ct);
        }
    }
}
