using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Application.Commands;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandHandler<CreateProduct> _createProduct;

        public ProductsController(ICommandHandler<CreateProduct> createProduct)
        {
            _createProduct = createProduct;
        }

        [HttpPost("product")] 
        public async Task<IActionResult> CreateProduct(CreateProduct cmd) { await _createProduct.HandleAsync(cmd); return Accepted(); }
    }
}
