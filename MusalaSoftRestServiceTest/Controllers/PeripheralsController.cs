using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MusalaSoftRestServiceTest.Models;

namespace MusalaSoftRestServiceTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeripheralsController : ControllerBase
    {
        private readonly RestServiceContext _context;

        public PeripheralsController(RestServiceContext context)
        {
            _context = context;
        }

        // GET: api/Peripherals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peripheral>>> GetPeripherals()
        {
            return await _context.Peripherals.ToListAsync();
        }

        // GET: api/Peripherals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Peripheral>> GetPeripheral(int id)
        {
            var peripheral = await _context.Peripherals.FindAsync(id);

            if (peripheral == null)
            {
                return NotFound();
            }

            return peripheral;
        }

        // PUT: api/Peripherals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeripheral(int id, Peripheral peripheral)
        {
            if (id != peripheral.UIDNumber)
            {
                return BadRequest();
            }

            _context.Entry(peripheral).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeripheralExists(id))
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

        // POST: api/Peripherals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Peripheral>> PostPeripheral(Peripheral peripheral)
        {
            _context.Peripherals.Add(peripheral);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPeripheral), new { id = peripheral.UIDNumber }, peripheral);
        }

        // DELETE: api/Peripherals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Peripheral>> DeletePeripheral(int id)
        {
            var peripheral = await _context.Peripherals.FindAsync(id);
            if (peripheral == null)
            {
                return NotFound();
            }

            _context.Peripherals.Remove(peripheral);
            await _context.SaveChangesAsync();

            return peripheral;
        }

        private bool PeripheralExists(int id)
        {
            return _context.Peripherals.Any(e => e.UIDNumber == id);
        }
    }
}
