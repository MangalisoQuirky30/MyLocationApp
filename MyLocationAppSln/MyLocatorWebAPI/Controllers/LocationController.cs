using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLocatorWebAPI.Data;
using MyLocatorWebAPI.Models;

namespace MyLocatorWebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly MyLocatorContext _context;

        public LocationController(MyLocatorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<LocationAPI>> GetAll() =>
            _context.AllLocationsAPI.ToList();

        // GET by ID action
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationAPI>> GetById(long id)
        {
            var user = await _context.AllLocationsAPI.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST action
        [HttpPost]
        public async Task<ActionResult<LocationAPI>> Create(LocationAPI user)
        {
            _context.AllLocationsAPI.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.ID }, user);
        }

        // PUT action
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, LocationAPI user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await _context.AllLocationsAPI.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.AllLocationsAPI.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}