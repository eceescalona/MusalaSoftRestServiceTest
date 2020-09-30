﻿using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MusalaSoftRestServiceTest.Models;

namespace MusalaSoftRestServiceTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewaysController : ControllerBase
    {
        private readonly RestServiceContext _context;

        public GatewaysController(RestServiceContext context)
        {
            _context = context;
        }

        // GET: api/Gateways
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gateway>>> GetGateways()
        {
            return await _context.Gateways.Include(g => g.Peripherals).ToListAsync();
        }

        // GET: api/Gateways/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gateway>> GetGateway(string id)
        {
            var gateway = await _context.Gateways.Include(g => g.Peripherals).SingleOrDefaultAsync(g => g.SerialNumber == id);

            if (gateway == null)
            {
                return NotFound();
            }

            return gateway;
        }

        // PUT: api/Gateways/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGateway(string id, Gateway gateway)
        {
            if (id != gateway.SerialNumber)
            {
                return BadRequest();
            }

            _context.Entry(gateway).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GatewayExists(id))
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

        // POST: api/Gateways
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Gateway>> PostGateway(Gateway gateway)
        {
            if (IPAddress.TryParse(gateway.IpAddress, out IPAddress address))
            {
                _context.Gateways.Add(gateway);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GatewayExists(gateway.SerialNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetGateway), new { id = gateway.SerialNumber }, gateway);
        }

        // DELETE: api/Gateways/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gateway>> DeleteGateway(string id)
        {
            var gateway = await _context.Gateways.FindAsync(id);
            if (gateway == null)
            {
                return NotFound();
            }

            _context.Gateways.Remove(gateway);
            await _context.SaveChangesAsync();

            return gateway;
        }

        private bool GatewayExists(string id)
        {
            return _context.Gateways.Any(e => e.SerialNumber == id);
        }
    }
}
