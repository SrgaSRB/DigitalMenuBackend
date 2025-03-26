using Digital_Menu.Data;
using Digital_Menu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Digital_Menu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly AppDbContext _db;

        public OrderController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("{localId}")]
        public IActionResult GetOrdersForLocal(string localId)
        {
            var tables = _db.Tables
                .Where(t => t.LocalId == localId)
                .Include(t => t.Orders)
                    .ThenInclude(o => o.Items)
                .ToList();

            return Ok(tables);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(CreateOrderDto order)
        {

            if (order == null) {
                return BadRequest(
                    new { message = "Order is required" }
                );
            }

            var table = await _db.Tables.
                Include(t => t.Orders)
                .FirstOrDefaultAsync(t => t.Id == order.TableId);

            if (table == null)
            {
                return NotFound(
                    new { message = "Table not found" }
                );
            }

            var newOrder = new Order
            {
                TableId = order.TableId,
                CreatedAt = DateTime.UtcNow,
                Status = "PORUCENO",
                Items = order.Items.Select(i => new OrderItem
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Note = i.Note,
                    Price = i.Price
                }).ToList()
            };

            _db.Orders.Add(newOrder);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "Order created",
                order = newOrder.Id
            });

        }
    }
}
