using Employee_Ticket_Booking.Data;
using Employee_Ticket_Booking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Ticket_Booking.Controllers
{
    [Route("api/[controller]")]
   // [Authorize]
    [ApiController]

    public class TravelRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravelRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/TravelRequest
        [HttpPost]
        public async Task<IActionResult> CreateTravelRequest([FromBody] TravelRequest request)
        {
            if (ModelState.IsValid)
            {
                _context.TravelRequests.Add(request);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Travel Request Created Successfully" });
            }
            return BadRequest(ModelState);
        }

        // GET: api/TravelRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelRequest>>> GetTravelRequests()
        {
            return await _context.TravelRequests.ToListAsync();
        }
    }
}

