using ClaimsTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
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
        public IActionResult SubmitClaim(string lecturerName, double hoursWorked, double hourlyRate, string notes, IFormFile document)
        {
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
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                Notes = notes,
                DocumentPath = documentPath,
                Status = ClaimStatus.Pending
            };

            // Add to the repository
            ClaimRepository.ClaimsList.Add(newClaim);

            return RedirectToAction("TrackClaims");
        }

        public IActionResult TrackClaims()
        {
            // Show all claims
            var lecturerClaims = ClaimRepository.ClaimsList.ToList();
            return View(lecturerClaims);
        }

    }
}
