using OrderManagementApp.DTOs;
using OrderManagementApp.Infrastructure;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Application.Queries
{
    public class GetCustomerByIdHandler : IQueryHandler<GetCustomerById, CustomerDto>
    {
        private readonly AppDbContext _db;
        public GetCustomerByIdHandler(AppDbContext db) => _db = db;
        public async Task<CustomerDto> HandleAsync(GetCustomerById query, CancellationToken ct = default)
        {
            var c = await _db.Customers.FindAsync(new object[] { query.Id }, ct);
            if (c == null) throw new KeyNotFoundException("Customer not found");
            return new CustomerDto(c.Id, c.Name, c.Email, c.CreatedAt);
        }
    }
}
