using System;
using System.IO;

namespace ClaimsTrackingSystem.Models
{
    public enum ClaimStatus
    {
        Pending,
        Verified,
        Accepted,
        Rejected
    }

    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public string Notes { get; set; } // Field for notes
        public string DocumentPath { get; set; } // Field for uploaded document
        public double Payout => HoursWorked * HourlyRate;

        // New property to get the file name from the path
        public string DocumentFileName
        {
            get
            {
                if (!string.IsNullOrEmpty(DocumentPath))
                {
                    return Path.GetFileName(DocumentPath);
                }
                return null;
            }
        }

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
    }

    public static class ClaimRepository
    {
        public static List<Claim> ClaimsList { get; set; } = new List<Claim>();
    }
}


