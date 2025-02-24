#nullable disable
using System;
using System.Collections.Generic;

namespace Employee_Ticket_Booking.Models;

    public partial class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int? UserTypeId { get; set; }

        public DateTime? CreatedAt { get; set; }
    }

