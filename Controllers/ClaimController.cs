using CMS_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using CMS_MVC.Models;
using System.IO;
using CMS_MVC.Controllers;
using System.Security.Claims;

namespace CMS_MVC.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(LecturerClaim claim, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // Save the file
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    claim.SupportingDocument = fileName;
                }

                claim.Status = "Pending";
                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TrackClaims));
            }
            return View(claim);
        }

        public IActionResult TrackClaims()
        {
            var claims = _context.LecturerClaims.ToList();
            return View(claims);
        }

        public IActionResult PendingClaims()
        {
            var pendingClaims = _context.LecturerClaims.Where(c => c.Status == "Pending").ToList();
            return View(pendingClaims);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int claimId)
        {
            var claim = await _context.LecturerClaims.FindAsync(claimId);
            if (claim != null)
            {
                claim.Status = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(PendingClaims));
        }

        [HttpPost]
        public async Task<IActionResult> RejectClaim(int claimId)
        {
            var claim = await _context.LecturerClaims.FindAsync(claimId);
            if (claim != null)
            {
                claim.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(PendingClaims));
        }

    }

}
