using System.ComponentModel.DataAnnotations;

namespace CMS_MVC.Models
{
    public class LecturerClaim
    {
        [Key]
        public int LecturerClaimId { get; set; }
        public string LecturerName { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; } = "Pending";
        public string SupportingDocument { get; set; }
    }
}
