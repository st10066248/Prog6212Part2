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

    public enum CommunicationMethod
    {
        Email,
        SMS
    }

    public enum Faculty
    {
        Commerce,
        ScienceAndTechnology,
        Law,
        Education,
        Humanities
    }

    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; }
        public string Surname { get; set; }
        public DateTime ClaimingForDate { get; set; } // The month being claimed for
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public string Notes { get; set; }
        public string DocumentPath { get; set; }
        public CommunicationMethod CommunicationMethod { get; set; } // Email or SMS
        public string ContactInfo { get; set; } // Either email or cellphone
        public Faculty Faculty { get; set; }
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        public double Payout => HoursWorked * HourlyRate;

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

        public bool IsValidClaim()
        {
            // Ensure the claim is not for the current month and hours worked <= 45
            if (ClaimingForDate >= DateTime.Now || HoursWorked > 45)
            {
                return false;
            }
            return true;
        }
    }

    public static class ClaimRepository
    {
        public static List<Claim> ClaimsList { get; set; } = new List<Claim>();
    }
}

