using EventManagement.Core.Models;
using EventManagement.Services;

namespace EventManagement.InfraStructure
{
    public class Registration
    {
        public int RegistrationId { get; set;}
        public int UserId { get; set;}
        public int EventId { get; set;}
        public DateTime RegistrationDate {  get; set;}

        public User User { get; set;}
        public Event Event { get; set;} 

        public Registration(int registrationId, int userId, int eventId) {
            RegistrationId = registrationId;
            UserId = userId;
            EventId = eventId;
        }

        public Registration() { }
    }
}
