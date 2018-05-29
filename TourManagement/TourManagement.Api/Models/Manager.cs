using System;

namespace TourManagement.Api.Models
{
    public class Manager : AuditableModel
    {
        public Guid ManagerId { get; set; }
        public string Name { get; set; }
    }
}