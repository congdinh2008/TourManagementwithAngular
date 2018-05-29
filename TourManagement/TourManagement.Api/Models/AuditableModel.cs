using System;
using System.ComponentModel.DataAnnotations;

namespace TourManagement.Api.Models
{
    public abstract class AuditableModel
    {
        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }
    }
}