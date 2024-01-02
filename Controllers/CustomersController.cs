using TaxiDispatch.API.DataTransferObjects.CustomerDto;

namespace TaxiDispatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DispatchtaxiContext _context;

        public CustomersController(DispatchtaxiContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerGetDto>>> GetCustomers()
        {
          if (_context.Customers == null)
          {
              return NotFound();
          }
            var customersData = await (from customer in _context.Customers
                                       join user in _context.Users on customer.UserId equals user.UserId
                                       select new CustomerGetDto
                                       {
                                           CustomerId = customer.CustomerId,
                                           UserId = customer.UserId,
                                           Address = customer.Address,
                                           Username = user.Username,
                                           Email = user.Email,
                                           FirstName = user.FirstName,
                                           LastName = user.LastName,
                                           Phone = user.Phone
                                       }).ToListAsync();

            return customersData;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerGetDto>> GetCustomer(int id)
        {
          if (_context.Customers == null)
          {
              return NotFound();
          }
            var customerData = await (from customer in _context.Customers
                                      join user in _context.Users on customer.UserId equals user.UserId
                                      where customer.CustomerId == id
                                      select new CustomerGetDto
                                      {
                                          CustomerId = customer.CustomerId,
                                          UserId = customer.UserId,
                                          Address = customer.Address,
                                          Username = user.Username,
                                          Email = user.Email,
                                          FirstName = user.FirstName,
                                          LastName = user.LastName,
                                          Phone = user.Phone
                                      }).FirstOrDefaultAsync();

            if (customerData == null)
            {
                return NotFound();
            }

            return customerData;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerPutDto request)
        {
            Customer? customer = await _context.Customers.FirstOrDefaultAsync(customer => customer.CustomerId == id);

            if (customer is null)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                customer.UserId = request.UserId;
                customer.Address = request.Address;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerPostDto customerDto)
        {
          if (_context.Customers == null)
          {
              return Problem("Entity set 'DispatchtaxiContext.Customers'  is null.");
          }
            Customer customer = new Customer
            {
                UserId = customerDto.UserId,
                Address = customerDto.Address,
            };

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
