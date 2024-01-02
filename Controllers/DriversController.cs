using TaxiDispatch.API.DataTransferObjects.DriverDto;
using TaxiDispatch.API.DataTransferObjects.UserDto;

namespace TaxiDispatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly DispatchtaxiContext _context;

        public DriversController(DispatchtaxiContext context)
        {
            _context = context;
        }

        // GET: api/Drivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverGetDto>>> GetDrivers()
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }

            var drivers = await (from driver in _context.Drivers
                                 join user in _context.Users on driver.UserId equals user.UserId
                                 select new DriverGetDto
                                 {
                                     DriverId = driver.DriverId,
                                     UserId = driver.UserId,
                                     LicenseNumber = driver.LicenseNumber,
                                     CarModel = driver.CarModel,
                                     CarPlateNumber = driver.CarPlateNumber,
                                     IsAvailability = driver.IsAvailability != 0,
                                     Username = user.Username,
                                     Email = user.Email,
                                     FirstName = user.FirstName,
                                     LastName = user.LastName,
                                     Phone = user.Phone
                                 }).ToListAsync();

            if (drivers == null)
            {
                return NotFound();
            }

            return drivers;
        }

        // GET: api/Drivers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DriverGetDto>>> GetDriver(int id)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }

            var drivers = await (from driver in _context.Drivers
                                 join user in _context.Users on driver.UserId equals user.UserId
                                 where driver.DriverId == id
                                 select new DriverGetDto
                                 {
                                     DriverId = driver.DriverId,
                                     LicenseNumber = driver.LicenseNumber,
                                     CarModel = driver.CarModel,
                                     CarPlateNumber = driver.CarPlateNumber,
                                     IsAvailability = driver.IsAvailability != 0,
                                     Username = user.Username,
                                     Email = user.Email,
                                     FirstName = user.FirstName,
                                     LastName = user.LastName,
                                     Phone = user.Phone
                                 }).ToListAsync();

            if (drivers == null)
            {
                return NotFound();
            }

            return drivers;
        }

        [HttpGet("byLicense/{licenseNumber}")]
        public async Task<ActionResult<DriverGetDto>> GetDriverByLicense(string licenseNumber)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            var driversByLicense = await (from user in _context.Users
                                          join driver in _context.Drivers on user.UserId equals driver.UserId
                                          where driver.LicenseNumber == licenseNumber
                                          select new DriverGetDto
                                          {
                                              UserId = user.UserId,
                                              DriverId = driver.DriverId,
                                              LicenseNumber = driver.LicenseNumber,
                                              CarModel = driver.CarModel,
                                              CarPlateNumber = driver.CarPlateNumber,
                                              Username = user.Username,
                                              Email = user.Email,
                                              FirstName = user.FirstName,
                                              LastName = user.LastName,
                                              Phone = user.Phone
                                          }).FirstOrDefaultAsync();

            if (driversByLicense == null)
            {
                return NotFound();
            }

            return driversByLicense;
        }

        [HttpGet("byCarPlateNumber/{plateNumber}")]
        public async Task<ActionResult<DriverGetDto>> GetDriverByCarPlateNumber(string plateNumber)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            var driversByPlateNumber = await (from user in _context.Users
                                          join driver in _context.Drivers on user.UserId equals driver.UserId
                                          where driver.CarPlateNumber == plateNumber
                                          select new DriverGetDto
                                          {
                                              DriverId = driver.DriverId,
                                              CarPlateNumber = driver.CarPlateNumber,
                                              CarModel = driver.CarModel,
                                              Username = user.Username,
                                              Email = user.Email,
                                              FirstName = user.FirstName,
                                              LastName = user.LastName,
                                              Phone = user.Phone
                                          }).FirstOrDefaultAsync();

            if (driversByPlateNumber == null)
            {
                return NotFound();
            }

            return driversByPlateNumber;
        }

        [HttpGet("byCarModel/{carModel}")]
        public async Task<ActionResult<DriverGetDto>> GetDriverByCarModel(string carModel)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            var driversByCarModel = await (from user in _context.Users
                                              join driver in _context.Drivers on user.UserId equals driver.UserId
                                              where driver.CarModel == carModel
                                              select new DriverGetDto
                                              {
                                                  DriverId= driver.DriverId,
                                                  CarPlateNumber = driver.CarPlateNumber,
                                                  CarModel = driver.CarModel,
                                                  Username = user.Username,
                                                  Email = user.Email,
                                                  FirstName = user.FirstName,
                                                  LastName = user.LastName,
                                                  Phone = user.Phone
                                              }).FirstOrDefaultAsync();

            if (driversByCarModel == null)
            {
                return NotFound();
            }

            return driversByCarModel;
        }

        // PUT: api/Drivers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("DriverPut/{id}")]
        public async Task<IActionResult> PutDriver(int id, DriverPutDto request)
        {
            Driver? driver = await _context.Drivers.FirstOrDefaultAsync(driver => driver.DriverId == id);

            if (driver is null)
            {
                return BadRequest();
            }

            _context.Entry(driver).State = EntityState.Modified;

            try
            {
                driver.UserId = request.UserId;
                driver.LicenseNumber = request.LicenseNumber;
                driver.CarModel = request.CarModel;
                driver.CarPlateNumber = request.CarPlateNumber;
                driver.IsAvailability = request.IsAvailability ? 1UL:0UL;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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

        // POST: api/Drivers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("DriverPost")]
        public async Task<ActionResult<Driver>> PostDriver(DriverPostDto driverPost)
        {
          if (_context.Drivers == null)
          {
              return Problem("Entity set 'DispatchtaxiContext.Drivers'  is null.");
          }

            Driver driver = new Driver()
            {
                UserId = driverPost.UserId,
                LicenseNumber = driverPost.LicenseNumber,
                CarModel = driverPost.CarModel,
                CarPlateNumber = driverPost.CarPlateNumber,
                IsAvailability = (bool)driverPost.IsAvailability ? 1UL : 0UL,
        };
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDriver), new { id = driver.DriverId }, driver);
        }

        // DELETE: api/Drivers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriverExists(int id)
        {
            return (_context.Drivers?.Any(e => e.DriverId == id)).GetValueOrDefault();
        }
    }
}
