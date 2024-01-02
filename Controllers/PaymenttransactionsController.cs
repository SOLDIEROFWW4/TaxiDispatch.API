using TaxiDispatch.API.DataTransferObjects.PaymentTransactionsDto;


namespace TaxiDispatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymenttransactionsController : ControllerBase
    {
        private readonly DispatchtaxiContext _context;

        public PaymenttransactionsController(DispatchtaxiContext context)
        {
            _context = context;
        }

        // GET: api/Paymenttransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentTransactionsGetDto>>> GetPaymenttransactions()
        {
          if (_context.Paymenttransactions == null)
          {
              return NotFound();
          }
            return await _context.Paymenttransactions.Select(transact => new PaymentTransactionsGetDto()
            {
                TransactionId = transact.TransactionId,
                OrderId = transact.OrderId,
                Amount = (int?)transact.Amount,
                PaymentDateTime = transact.PaymentDateTime,
                IsPaymentCompleted = transact.IsPaymentCompleted !=0,
            }).ToListAsync();
        }

        // GET: api/Paymenttransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentTransactionsGetDto>> GetPaymenttransaction(int id)
        {
          if (_context.Paymenttransactions == null)
          {
              return NotFound();
          }
            var transact = await _context.Paymenttransactions.FindAsync(id);

            if (transact == null)
            {
                return NotFound();
            }

            return new PaymentTransactionsGetDto
            {
                TransactionId = transact.TransactionId,
                OrderId = transact.OrderId,
                Amount = (int?)transact.Amount,
                PaymentDateTime = transact.PaymentDateTime,
                IsPaymentCompleted = transact.IsPaymentCompleted != 0,
            };
        }

        // GET: api/Paymenttransactions/completion?isPaymentCompleted=true
        [HttpGet("byPaymentCompleted/{isPaymentCompleted}")]
        public async Task<ActionResult<IEnumerable<PaymentTransactionsGetDto>>> GetPaymenttransactionByCompletion(bool isPaymentCompleted)
        {
            var paymentTransactions = await _context.Paymenttransactions
                .Where(p => (int)p.IsPaymentCompleted == (isPaymentCompleted ? 1 : 0))
                .Select(transact => new PaymentTransactionsGetDto
                {
                    TransactionId = transact.TransactionId,
                    OrderId = transact.OrderId,
                    Amount = (int?)transact.Amount,
                    PaymentDateTime = transact.PaymentDateTime,
                    IsPaymentCompleted = transact.IsPaymentCompleted != 0,
                })
                .ToListAsync();

            if (paymentTransactions == null)
            {
                return NotFound();
            }

            return paymentTransactions;
        }

        // GET: api/Paymenttransactions/datetime
        [HttpGet("ByDateTime")]
        public async Task<ActionResult<IEnumerable<PaymentTransactionsGetDto>>> GetPaymenttransactionsByDateTime()
        {
            var paymentTransactions = await _context.Paymenttransactions
                .OrderByDescending(p => p.PaymentDateTime)
                .Select(transact => new PaymentTransactionsGetDto
                {
                    TransactionId = transact.TransactionId,
                    OrderId = transact.OrderId,
                    Amount = (int?)transact.Amount,
                    PaymentDateTime = transact.PaymentDateTime,
                    IsPaymentCompleted = transact.IsPaymentCompleted != 0,
                })
                .ToListAsync();

            if (paymentTransactions == null)
            {
                return NotFound();
            }

            return paymentTransactions;
        }

        // PUT: api/Paymenttransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymenttransaction(int id, PaymentTransactionsPutDto request)
        {
            Paymenttransaction transact = await _context.Paymenttransactions.FirstOrDefaultAsync(transact => transact.TransactionId == id);

            if (transact is null)
            {
                return BadRequest();
            }

            _context.Entry(transact).State = EntityState.Modified;

            try
            {
                transact.OrderId = request.OrderId;
                transact.Amount = request.Amount;
                transact.PaymentDateTime = request.PaymentDateTime;
                transact.IsPaymentCompleted = request.IsPaymentCompleted ? 1UL : 0UL;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymenttransactionExists(id))
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

        // POST: api/Paymenttransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Paymenttransaction>> PostPaymenttransaction(PaymentTransactionsPostDto transactPost)
        {
          if (_context.Paymenttransactions == null)
          {
              return Problem("Entity set 'DispatchtaxiContext.Paymenttransactions'  is null.");
          }

            Paymenttransaction transact = new Paymenttransaction()
            {
                OrderId = transactPost.OrderId,
                Amount = (decimal)transactPost.Amount,
                PaymentDateTime = (DateTime)transactPost.PaymentDateTime,
                IsPaymentCompleted = (bool)transactPost.IsPaymentCompleted ? 1UL : 0UL,
            };
            _context.Paymenttransactions.Add(transact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaymenttransaction), new { id = transact.TransactionId }, transact);
        }

        // DELETE: api/Paymenttransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymenttransaction(int id)
        {
            if (_context.Paymenttransactions == null)
            {
                return NotFound();
            }
            var paymenttransaction = await _context.Paymenttransactions.FindAsync(id);
            if (paymenttransaction == null)
            {
                return NotFound();
            }

            _context.Paymenttransactions.Remove(paymenttransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymenttransactionExists(int id)
        {
            return (_context.Paymenttransactions?.Any(e => e.TransactionId == id)).GetValueOrDefault();
        }
    }
}
