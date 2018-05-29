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

        public DateTime UpdateOn { get; set; }

        public string UpdateBy { get; set; }
    }
}