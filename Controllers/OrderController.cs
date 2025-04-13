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

            foreach(var table in tables)
            {
                table.Orders = table.Orders.Where(o => o.isDeleted == false).ToList();
            }

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

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            var order = await _db.Orders.FindAsync(id);

            if (order == null) 
                return NotFound();

            order.Status = dto.Status;
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _db.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            if(order.isDeleted == true)
            {
                return Conflict("Order was alredy deleted");
            }

            order.isDeleted = true;
            await _db.SaveChangesAsync();

            return Ok();

        }

        [HttpGet("{localId}/history")]
        public async Task<IActionResult> GetHistory(string localId)
        {
            var finishedOrders = await _db.Orders
                .Include(o => o.Table)
                .Include(o => o.Items)
                .Where(o => o.isDeleted && o.Table.LocalId == localId)
                .ToListAsync();

            return Ok(finishedOrders);
        }
    }
}
