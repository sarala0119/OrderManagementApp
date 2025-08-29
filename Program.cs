using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OrderManagementApp.Application.Commands;
using OrderManagementApp.Application.Queries;
using OrderManagementApp.DTOs;
using OrderManagementApp.Infrastructure;
using static OrderManagementApp.Application.Abstractions.Cqrs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ICommandHandler<CreateCustomer>, CreateCustomerHandler>();
builder.Services.AddTransient<ICommandHandler<CreateProduct>, CreateProductHandler>();
builder.Services.AddTransient<ICommandHandler<CreateOrder>, CreateOrderHandler>();
builder.Services.AddTransient<ICommandHandler<AddOrderItem>, AddOrderItemHandler>();
builder.Services.AddTransient<ICommandHandler<PlaceOrder>, PlaceOrderHandler>();
builder.Services.AddTransient<ICommandHandler<PayOrder>, PayOrderHandler>();
builder.Services.AddTransient<ICommandHandler<CancelOrder>, CancelOrderHandler>();

builder.Services.AddTransient<IQueryHandler<GetOrderById, OrderDto>, GetOrderByIdHandler>();
builder.Services.AddTransient<IQueryHandler<GetCustomerById, CustomerDto>, GetCustomerByIdHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
