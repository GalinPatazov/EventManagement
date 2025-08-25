using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Core.DTOs
{
    public class EventDTO
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int SpeakerId { get; set; }
        public string? SpeakerName { get; set; }
    }
}
