using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrderDTO{
        public int Id { get; set; } 
        public string Name { get; set; }
    }
    [ApiController]
    [Route("api/")]

    public class OrderRestController : ControllerBase
    {
        private readonly OrderServiceContext _context = new OrderServiceContext();

        [HttpGet("orders")]
        public async Task<List<OrderDTO>> Index()
        {
            return await _context.Orders.Select(o => new OrderDTO(){ Id = o.OrderId, Name = o.OrderName}).ToListAsync();
        }
    }
}
