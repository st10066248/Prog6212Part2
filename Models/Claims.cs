using System;
using System.Collections.Generic;

namespace ClaimsTrackingSystem.Models
{
    public enum ClaimStatus
    {
        Pending,
        Approved,
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
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
    }

    public static class ClaimRepository
    {
        public static List<Claim> ClaimsList { get; set; } = new List<Claim>();
    }
}
