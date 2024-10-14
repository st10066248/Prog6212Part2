using ClaimsTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsTrackingSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult ViewClaims(string role)
        {
            // Show all claims (pending, approved, or rejected)
            var claims = ClaimRepository.ClaimsList.ToList();
            ViewData["Role"] = role;
            return View(claims);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id, string role)
        {
            var claim = ClaimRepository.ClaimsList.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved;
            }
            return RedirectToAction("ViewClaims", new { role = role });
        }

        [HttpPost]
        public IActionResult RejectClaim(int id, string role)
        {
            var claim = ClaimRepository.ClaimsList.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected;
            }
            return RedirectToAction("ViewClaims", new { role = role });
        }
    }
}
