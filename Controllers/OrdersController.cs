using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Application.Commands;
using OrderManagementApp.Application.Queries;
using OrderManagementApp.DTOs;
using static OrderManagementApp.Application.Abstractions.Cqrs;

namespace OrderManagementApp.Controllers
{
    public class OrdersController : ControllerBase
    {
        private readonly ICommandHandler<CreateOrder> _createOrder;
        private readonly ICommandHandler<AddOrderItem> _addItem;
        private readonly ICommandHandler<PlaceOrder> _placeOrder;
        private readonly ICommandHandler<PayOrder> _payOrder;
        private readonly ICommandHandler<CancelOrder> _cancelOrder;
        private readonly IQueryHandler<GetOrderById, OrderDto> _getOrder;

        public OrdersController(ICommandHandler<CreateOrder> createOrder, ICommandHandler<AddOrderItem> addItem, ICommandHandler<PlaceOrder> placeOrder,
ICommandHandler<PayOrder> payOrder, ICommandHandler<CancelOrder> cancelOrder, IQueryHandler<GetOrderById, OrderDto> getOrder)
        {
            _createOrder = createOrder;
            _addItem = addItem; 
            _placeOrder = placeOrder; 
            _payOrder = payOrder; 
            _cancelOrder = cancelOrder;
            _getOrder = getOrder;
        }
        

        [HttpPost("Order")]
        public async Task<IActionResult> CreateOrder(CreateOrder cmd) { await _createOrder.HandleAsync(cmd); return Accepted(); }

        [HttpPost("orders/items")] 
        public async Task<IActionResult> AddItem([FromBody] AddOrderItem cmd) 
        {
                       
            await _addItem.HandleAsync(cmd); return Accepted();
        }
        [HttpPost("orders/place")] 
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrder cmd)
        { 
             await _placeOrder.HandleAsync(cmd); return Accepted(); 
        }
        [HttpPost("orders/pay")] public async Task<IActionResult> PayOrder([FromBody] PayOrder cmd) {  await _payOrder.HandleAsync(cmd); return Accepted(); }
        [HttpPost("orders/cancel")] public async Task<IActionResult> CancelOrder([FromBody] CancelOrder cmd) { await _cancelOrder.HandleAsync(cmd); return Accepted(); }
        [HttpGet("orders/{id}")] 
        public async Task<OrderDto> GetOrder([FromQuery] Guid id) => await _getOrder.HandleAsync(new GetOrderById(id));
    }
}
