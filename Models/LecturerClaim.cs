namespace ClaimsManagamentSystem.Models
{
    public class LecturerClaim
    {
        public int ClaimId { get; set; }
        public string LecturerName { get; set; }
        public DateTime DateSubmitted { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string AdditionalNotes { get; set; }
        public string UploadedFilePath { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected
    }

}
