using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrderServiceGrpc : OrderCRUD.OrderCRUDBase
    {
        OrderServiceContext _context = new OrderServiceContext();
        public override async Task<Orders> SelectAll(Empty request, ServerCallContext context)
        {
            Orders response = new Orders();
            var result = await _context.Orders.Select(o=>new Order() { OrderId = o.OrderId,  OrderName = o.OrderName }).ToListAsync();
            response.Items.AddRange(result);
            return response; 
        }
    }
}
