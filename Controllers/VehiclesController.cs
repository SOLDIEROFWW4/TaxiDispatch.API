using TaxiDispatch.API.DataTransferObjects.VehicleDto;

namespace TaxiDispatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly DispatchtaxiContext _context;

        public VehiclesController(DispatchtaxiContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleGetDto>>> GetVehicles()
        {
          if (_context.Vehicles == null)
          {
              return NotFound();
          }
            return await _context.Vehicles.Select(vehicle => new VehicleGetDto()
            {
                VehicleId = vehicle.VehicleId,
                DriverId = vehicle.DriverId,
                VehicleType = vehicle.VehicleType,
                Year = vehicle.Year,
                Color = vehicle.Color,
            }).ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleGetDto>> GetVehicle(int id)
        {
          if (_context.Vehicles == null)
          {
              return NotFound();
          }
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return new VehicleGetDto()
            {
                VehicleId = vehicle.VehicleId,
                DriverId = vehicle.DriverId,
                VehicleType = vehicle.VehicleType,
                Year = vehicle.Year,
                Color = vehicle.Color,
            };
        }

        [HttpGet("ByVehicleType/{vehicleType}")]
        public async Task<ActionResult<IEnumerable<VehicleGetDto>>> GetVehicleByType(string vehicleType)
        {
            var vehicles = await _context.Vehicles
                .Where(v => v.VehicleType == vehicleType)
                .Select(vehicle => new VehicleGetDto
                {
                    VehicleId = vehicle.VehicleId,
                    DriverId = vehicle.DriverId,
                    VehicleType = vehicle.VehicleType,
                    Year = vehicle.Year,
                    Color = vehicle.Color,
                })
                .ToListAsync();

            if (vehicles == null)
            {
                return NotFound();
            }

            return vehicles;
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, VehiclePutDto request)
        {
            Vehicle? vehicle = await _context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.VehicleId == id);

            if (vehicle is null)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                vehicle.DriverId = request.DriverId;
                vehicle.VehicleType = request.VehicleType;
                vehicle.Year = request.Year;
                vehicle.Color = request.Color;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(VehiclePostDto vehiclePost)
        {
          if (_context.Vehicles == null)
          {
              return Problem("Entity set 'DispatchtaxiContext.Vehicles'  is null.");
          }

            Vehicle vehicle = new Vehicle()
            {
                DriverId = vehiclePost.DriverId,
                VehicleType = vehiclePost.VehicleType,
                Year = (int)vehiclePost.Year,
                Color = vehiclePost.Color,
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.VehicleId }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(int id)
        {
            return (_context.Vehicles?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }
    }
}
