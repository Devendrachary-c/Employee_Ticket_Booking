using Employee_Ticket_Booking.Data;
using Employee_Ticket_Booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Employee_Ticket_Booking.Controllers
{
    [Route("api/usertypes")]
    [ApiController]
    public class UserTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/usertypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetUserTypes()
        {
            return await _context.UserTypes.ToListAsync();
        }

        // GET: api/usertypes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserType>> GetUserType(int id)
        {
            var userType = await _context.UserTypes.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }
            return userType;
        }

        // POST: api/usertypes
        [HttpPost]
        public async Task<ActionResult<UserType>> CreateUserType(UserType userType)
        {
            _context.UserTypes.Add(userType);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserType), new { id = userType.Id }, userType);
        }

        // PUT: api/usertypes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserType(int id, UserType userType)
        {
            if (id != userType.Id)
            {
                return BadRequest();
            }

            _context.Entry(userType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/usertypes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserType(int id)
        {
            var userType = await _context.UserTypes.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            _context.UserTypes.Remove(userType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
