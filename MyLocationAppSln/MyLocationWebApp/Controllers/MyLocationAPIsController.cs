using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoPets.Api.Data;
using MyLocationWebApp.Models;

namespace MyLocationWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyLocationAPIsController : ControllerBase
    {
        private readonly ContosoPetsContext _context;

        public MyLocationAPIsController(ContosoPetsContext context)
        {
            _context = context;
        }

        // GET: api/MyLocationAPIs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyLocationAPI>>> GetLocations()
        {
            return await _context.Locations.ToListAsync();
        }

        // GET: api/MyLocationAPIs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyLocationAPI>> GetMyLocationAPI(int id)
        {
            var myLocationAPI = await _context.Locations.FindAsync(id);

            if (myLocationAPI == null)
            {
                return NotFound();
            }

            return myLocationAPI;
        }

        // PUT: api/MyLocationAPIs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMyLocationAPI(int id, MyLocationAPI myLocationAPI)
        {
            if (id != myLocationAPI.UserId)
            {
                return BadRequest();
            }

            _context.Entry(myLocationAPI).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyLocationAPIExists(id))
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

        // POST: api/MyLocationAPIs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MyLocationAPI>> PostMyLocationAPI(MyLocationAPI myLocationAPI)
        {
            _context.Locations.Add(myLocationAPI);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMyLocationAPI", new { id = myLocationAPI.UserId }, myLocationAPI);
        }

        // DELETE: api/MyLocationAPIs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MyLocationAPI>> DeleteMyLocationAPI(int id)
        {
            var myLocationAPI = await _context.Locations.FindAsync(id);
            if (myLocationAPI == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(myLocationAPI);
            await _context.SaveChangesAsync();

            return myLocationAPI;
        }

        private bool MyLocationAPIExists(int id)
        {
            return _context.Locations.Any(e => e.UserId == id);
        }
    }
}
