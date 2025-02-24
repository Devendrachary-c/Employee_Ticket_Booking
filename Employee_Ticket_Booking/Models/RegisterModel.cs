
using System;
using System.Collections.Generic;
namespace Employee_Ticket_Booking.Models;

public  class RegisterModel
{
    public  required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
}
