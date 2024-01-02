using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TaxiDispatch.API.DataTransferObjects.RatingDto;

namespace TaxiDispatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly DispatchtaxiContext _context;

        public RatingsController(DispatchtaxiContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingGetDto>>> GetRatings()
        {
          if (_context.Ratings == null)
          {
              return NotFound();
          }
            return await _context.Ratings.Select(rating => new RatingGetDto()
            {
                RatingId = rating.RatingId,
                OrderId = rating.OrderId,
                RatingValue = rating.RatingValue,
                Comment = rating.Comment,
            }).ToListAsync();
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingGetDto>> GetRating(int id)
        {
          if (_context.Ratings == null)
          {
              return NotFound();
          }
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return new RatingGetDto()
            {
                RatingId = rating.RatingId,
                OrderId = rating.OrderId,
                RatingValue = rating.RatingValue,
                Comment = rating.Comment,
            };
        }

        // GET: api/Ratings/value
        [HttpGet("ByRatingValue/{RatingValue}")]
        public async Task<ActionResult<IEnumerable<RatingGetDto>>> GetRatingsByValue()
        {
            var ratings = await _context.Ratings
                .OrderByDescending(r => r.RatingValue)
                .Select(rating => new RatingGetDto
                {
                    RatingId = rating.RatingId,
                    OrderId = rating.OrderId,
                    RatingValue = rating.RatingValue,
                    Comment = rating.Comment,
                })
                .ToListAsync();

            if (ratings == null)
            {
                return NotFound();
            }

            return ratings;
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, RatingPutDto request)
        {
            Rating? rating = await _context.Ratings.FirstOrDefaultAsync(rating => rating.RatingId == id);

            if (rating is null)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                rating.OrderId = request.OrderId;
                rating.RatingValue = request.RatingValue;
                rating.Comment = request.Comment;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(RatingPostDto ratingPost)
        {
          if (_context.Ratings == null)
          {
              return Problem("Entity set 'DispatchtaxiContext.Ratings'  is null.");
          }

            Rating rating = new Rating()
            {
                OrderId = ratingPost.OrderId,
                RatingValue = ratingPost.RatingValue,
                Comment = ratingPost.Comment,

            };
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRating), new { id = rating.RatingId }, rating);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            if (_context.Ratings == null)
            {
                return NotFound();
            }
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(int id)
        {
            return (_context.Ratings?.Any(e => e.RatingId == id)).GetValueOrDefault();
        }
    }
}
