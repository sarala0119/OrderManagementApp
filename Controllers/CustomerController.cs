using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Application.Commands;
using OrderManagementApp.Application.Queries;
using OrderManagementApp.DTOs;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICommandHandler<CreateCustomer> _createCustomer;
        private readonly IQueryHandler<GetCustomerById, CustomerDto> _getCustomer;

        public CustomerController(ICommandHandler<CreateCustomer> createCustomer, IQueryHandler<GetCustomerById, CustomerDto> getCustomer)
        {
            _createCustomer = createCustomer;
            _getCustomer = getCustomer;
        }

        [HttpPost("customers")] 
        public async Task<IActionResult> CreateCustomer(CreateCustomer cmd) { await _createCustomer.HandleAsync(cmd); return Accepted(); }
    }
}
