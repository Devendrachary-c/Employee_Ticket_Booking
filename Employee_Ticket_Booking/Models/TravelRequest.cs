#nullable disable
using System;
using System.Collections.Generic;

namespace Employee_Ticket_Booking.Models;

    public partial class TravelRequest
    {
    public int? Id { get; set; }

    public string EmployeeName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Destination { get; set; }

    public DateTime? TravelDate { get; set; }

    public string Purpose { get; set; }
}

