
using System;
using System.Collections.Generic;
namespace Employee_Ticket_Booking.Models;

    public partial class Employee
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public  string Email { get; set; }
        public string? Phone { get; set; }

        public decimal Salary { get; set; }
    }

