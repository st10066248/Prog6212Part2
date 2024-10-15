using ClaimsTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Linq;

namespace ClaimsTrackingSystem.Controllers
{
    public class LecturerController : Controller
    {
        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClaim(string lecturerName, string surname, DateTime claimingForDate, double hoursWorked, double hourlyRate, string notes, IFormFile document, CommunicationMethod communicationMethod, string contactInfo, Faculty faculty)
        {
            // Check if the claim is valid (not for the current month and <= 45 hours)
            if (claimingForDate >= DateTime.Now || hoursWorked > 45)
            {
                ViewData["Error"] = "Invalid claim: You cannot claim for the current month, and hours worked cannot exceed 45.";
                return View();
            }

            // Save the uploaded file
            string documentPath = null;
            if (document != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);
                documentPath = Path.Combine(uploadsFolder, document.FileName);

                using (var fileStream = new FileStream(documentPath, FileMode.Create))
                {
                    document.CopyTo(fileStream);
                }
            }

            // Create new claim
            var newClaim = new Claim
            {
                Id = ClaimRepository.ClaimsList.Count + 1,
                LecturerName = lecturerName,
                Surname = surname,
                ClaimingForDate = claimingForDate,
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                Notes = notes,
                DocumentPath = documentPath,
                CommunicationMethod = communicationMethod,
                ContactInfo = contactInfo,
                Faculty = faculty,
                Status = ClaimStatus.Pending
            };

            // Add to the repository
            ClaimRepository.ClaimsList.Add(newClaim);

            return RedirectToAction("TrackClaims");
        }

        public IActionResult TrackClaims(string lecturerName = null)
        {
            var lecturerClaims = ClaimRepository.ClaimsList.ToList();

            if (!string.IsNullOrEmpty(lecturerName))
            {
                lecturerClaims = lecturerClaims
                    .Where(c => c.LecturerName.ToLower().Contains(lecturerName.ToLower()))
                    .ToList();
            }

            ViewData["LecturerName"] = lecturerName;
            return View(lecturerClaims);
        }
    }
}

