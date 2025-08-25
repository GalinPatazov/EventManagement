using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Core.DTOs
{
    public class RegistrationDTO
    {
        public int RegistrationId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? UserName { get; set; }
        public string? EventTitle { get; set; }
    }
}
