using TaxiDispatch.API.DataTransferObjects.OrderDto;

namespace TaxiDispatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DispatchtaxiContext _context;

        public OrdersController(DispatchtaxiContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderGetDto>>> GetOrders()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            return await _context.Orders.Select(order => new OrderGetDto()
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                DriverId = order.DriverId,
                PickupLocation = order.PickupLocation,
                Destination = order.Destination,
                OrderDateTime = order.OrderDateTime,
                Status = order.Status,
            }).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderGetDto>> GetOrder(int id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return new OrderGetDto()
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                DriverId = order.DriverId,
                PickupLocation = order.PickupLocation,
                Destination = order.Destination,
                OrderDateTime = order.OrderDateTime,
                Status = order.Status,
            };
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderPutDto request)
        {
            Order? order = await _context.Orders.FirstOrDefaultAsync(order => order.OrderId == id);

            if (order is null)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                order.CustomerId = request.CustomerId;
                order.DriverId = request.DriverId;
                order.PickupLocation = request.PickupLocation;
                order.Destination = request.Destination;
                order.OrderDateTime = (DateTime)request.OrderDateTime;
                order.Status = request.Status;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderPostDto orderPost)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'DispatchtaxiContext.Orders'  is null.");
          }

            Order order = new Order()
            {
                CustomerId = orderPost.CustomerId,
                DriverId = orderPost.DriverId,
                PickupLocation = orderPost.PickupLocation,
                Destination = orderPost.Destination,
                OrderDateTime = (DateTime)orderPost.OrderDateTime,
                Status = orderPost.Status,
            };


            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
