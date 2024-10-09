using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ClaimsManagementSystem.Models
{
    public class LecturerClaim
    {
        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "Please enter the hours worked.")]
        public decimal HoursWorked { get; set; }

        [Required(ErrorMessage = "Please enter the hourly rate.")]
        public decimal HourlyRate { get; set; }

        public string AdditionalNotes { get; set; }

        public string SupportingDocumentPath { get; set; } // To store the file path
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending; // Default status
    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}

