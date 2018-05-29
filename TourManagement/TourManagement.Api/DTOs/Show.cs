using System;

namespace TourManagement.Api.DTOs
{
    public class Show
    {
        public Guid ShowId { get; set; }
        public string Venue { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
