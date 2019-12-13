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
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly RegistrationContext _context;

        public RegistrationController(RegistrationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<RegistrationAPI>> GetAll() =>
            _context.RegistrationAPIUsers.ToList();

        // GET by ID action
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistrationAPI>> GetById(long id)
        {
            var user = await _context.RegistrationAPIUsers.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST action
        [HttpPost]
        public async Task<ActionResult<RegistrationAPI>> Create(RegistrationAPI user)
        {
            _context.RegistrationAPIUsers.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.ID }, user);
        }

        // PUT action
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, RegistrationAPI user)
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
            var product = await _context.RegistrationAPIUsers.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.RegistrationAPIUsers.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
