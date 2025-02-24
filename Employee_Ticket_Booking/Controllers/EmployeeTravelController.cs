using Employee_Ticket_Booking.Data;
using Employee_Ticket_Booking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Employee_Ticket_Booking.Controllers
{
    //[Authorize(Roles = "Admin,Employee")]
    // Require authentication
    [Route("api/EmployeeTravel")]
    [ApiController]

    public class EmployeeTravelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTravelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/employee-travel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeTravel>>> GetTravelRequests()
        {
            return await _context.EmployeeTravels.ToListAsync();
        }

        // GET: api/employee-travel/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTravel>> GetTravelRequest(int id)
        {
            var travelRequest = await _context.EmployeeTravels.Include(t => t.Username)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (travelRequest == null)
                return NotFound();

            return travelRequest;
        }

        // POST: api/employee-travel (Create Travel Request)
        [HttpPost]
        public async Task<ActionResult<EmployeeTravel>> CreateTravelRequest(EmployeeTravel travelRequest)
        {
            // Get the logged-in user ID
            var userId = int.Parse(User.FindFirst("UserId")?.Value);

            travelRequest.UserId = userId.ToString();
            travelRequest.Status = travelRequest.Status; // Default status

            _context.EmployeeTravels.Add(travelRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTravelRequest), new { id = travelRequest.Id }, travelRequest);
        }

        // PUT: api/employee-travel/{id} (Update Travel Request)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTravelRequest(int id, EmployeeTravel travelRequest)
        {
            if (id != travelRequest.Id)
                return BadRequest();

            var existingTravel = await _context.EmployeeTravels.FindAsync(id);
            if (existingTravel == null)
                return NotFound();

            // Allow only the owner or an Admin to update
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (existingTravel.UserId != userId.ToString() && userRole != "Admin")
                return Forbid();

            existingTravel.Destination = travelRequest.Destination;
            existingTravel.StartDate = travelRequest.StartDate;
            existingTravel.EndDate = travelRequest.EndDate;
            existingTravel.Purpose = travelRequest.Purpose;
            existingTravel.Status = travelRequest.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/employee-travel/{id} (Delete Travel Request)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelRequest(int id)
        {
            var travelRequest = await _context.EmployeeTravels.FindAsync(id);
            if (travelRequest == null)
                return NotFound();

            _context.EmployeeTravels.Remove(travelRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/employee-travel/{id}/approve (Manager/Admin Approval)
        [Authorize(Roles = "Manager,Admin")]
        [HttpPatch("{id}/approve")]
        public async Task<IActionResult> ApproveTravelRequest(int id)
        {
            var travelRequest = await _context.EmployeeTravels.FindAsync(id);
            if (travelRequest == null)
                return NotFound();

            travelRequest.Status = travelRequest.Status;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Travel request approved." });
        }

        // PATCH: api/employee-travel/{id}/reject (Manager/Admin Rejection)
        [Authorize(Roles = "Manager,Admin")]
        [HttpPatch("{id}/reject")]
        public async Task<IActionResult> RejectTravelRequest(int id)
        {
            var travelRequest = await _context.EmployeeTravels.FindAsync(id);
            if (travelRequest == null)
                return NotFound();

            travelRequest.Status = travelRequest.Status;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Travel request rejected." });
        }
    }
}

