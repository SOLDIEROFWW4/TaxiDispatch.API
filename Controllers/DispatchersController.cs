using TaxiDispatch.API.DataTransferObjects.DispatcherDto;
using TaxiDispatch.API.DataTransferObjects.VehicleDto;

namespace TaxiDispatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchersController : ControllerBase
    {
        private readonly DispatchtaxiContext _context;

        public DispatchersController(DispatchtaxiContext context)
        {
            _context = context;
        }

        // GET: api/Dispatchers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DispatcherGetDto>>> GetDispatchers()
        {
          if (_context.Dispatchers == null)
          {
              return NotFound();
          }
            var dispatchersData = await (from dispatcher in _context.Dispatchers
                                         join user in _context.Users on dispatcher.UserId equals user.UserId
                                         select new DispatcherGetDto
                                         {
                                             DispatcherId = dispatcher.DispatcherId,
                                             UserId = dispatcher.UserId,
                                             Department = dispatcher.Department,
                                             ShiftTime = dispatcher.ShiftTime,
                                             Username = user.Username,
                                             Email = user.Email,
                                             FirstName = user.FirstName,
                                             LastName = user.LastName,
                                             Phone = user.Phone
                                         }).ToListAsync();

            return dispatchersData;
        }

        // GET: api/Dispatchers/10
        [HttpGet("BaseGet")]
        public async Task<ActionResult<IEnumerable<DispatcherBaseGetDto>>> GetDispatchersBase()
        {
            if (_context.Dispatchers == null)
            {
                return NotFound();
            }
            return await _context.Dispatchers.Select(dispatcher => new DispatcherBaseGetDto()
            {
                DispatcherId = dispatcher.DispatcherId,
                UserId = dispatcher.UserId,
                Department = dispatcher.Department,
                ShiftTime = dispatcher.ShiftTime,
            }).ToListAsync();
        }

        // GET: api/Dispatchers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DispatcherGetDto>> GetDispatcher(int id)
        {
          if (_context.Dispatchers == null)
          {
              return NotFound();
          }
            var dispatcherData = await (from dispatcher in _context.Dispatchers
                                        join user in _context.Users on dispatcher.UserId equals user.UserId
                                        where dispatcher.DispatcherId == id
                                        select new DispatcherGetDto
                                        {
                                            DispatcherId = dispatcher.DispatcherId,
                                            UserId = dispatcher.UserId,
                                            Department = dispatcher.Department,
                                            ShiftTime = dispatcher.ShiftTime,
                                            Username = user.Username,
                                            Email = user.Email,
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,
                                            Phone = user.Phone
                                        }).FirstOrDefaultAsync();

            if (dispatcherData == null)
            {
                return NotFound();
            }

            return dispatcherData;
        }

        // GET: api/Dispatchers?department={department}
        [HttpGet("byDepartment/{department}")]
        public async Task<ActionResult<IEnumerable<DispatcherGetDto>>> GetDispatchersByDepartment(string department)
        {
            var dispatchersData = await (from dispatcher in _context.Dispatchers
                                         join user in _context.Users on dispatcher.UserId equals user.UserId
                                         where dispatcher.Department == department
                                         select new DispatcherGetDto
                                         {
                                             DispatcherId = dispatcher.DispatcherId,
                                             UserId = dispatcher.UserId,
                                             Department = dispatcher.Department,
                                             ShiftTime = dispatcher.ShiftTime,
                                             Username = user.Username,
                                             Email = user.Email,
                                             FirstName = user.FirstName,
                                             LastName = user.LastName,
                                             Phone = user.Phone
                                         }).ToListAsync();

            if (dispatchersData == null || dispatchersData.Count == 0)
            {
                return NotFound();
            }

            return dispatchersData;
        }

        // PUT: api/Dispatchers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispatcher(int id, DispatcherPutDto request)
        {
            Dispatcher? dispatcher = await _context.Dispatchers.FirstOrDefaultAsync(dispatcher => dispatcher.DispatcherId == id);

            if (dispatcher is null)
            {
                return BadRequest();
            }

            _context.Entry(dispatcher).State = EntityState.Modified;

            try
            {
                dispatcher.UserId = dispatcher.UserId;
                dispatcher.Department = dispatcher.Department;
                dispatcher.ShiftTime = dispatcher.ShiftTime;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispatcherExists(id))
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

        // POST: api/Dispatchers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DispatcherPostDto>> PostDispatcher(Dispatcher dispatcherPost)
        {
          if (_context.Dispatchers == null)
          {
              return Problem("Entity set 'DispatchtaxiContext.Dispatchers'  is null.");
          }

            Dispatcher dispatcher = new Dispatcher()
            {
                UserId = dispatcherPost.UserId,
                Department = dispatcherPost.Department,
                ShiftTime = dispatcherPost.ShiftTime,
            };

            _context.Dispatchers.Add(dispatcher);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDispatchersBase), new { id = dispatcher.DispatcherId }, dispatcher);
        }

        // DELETE: api/Dispatchers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispatcher(int id)
        {
            if (_context.Dispatchers == null)
            {
                return NotFound();
            }
            var dispatcher = await _context.Dispatchers.FindAsync(id);
            if (dispatcher == null)
            {
                return NotFound();
            }

            _context.Dispatchers.Remove(dispatcher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DispatcherExists(int id)
        {
            return (_context.Dispatchers?.Any(e => e.DispatcherId == id)).GetValueOrDefault();
        }
    }
}
