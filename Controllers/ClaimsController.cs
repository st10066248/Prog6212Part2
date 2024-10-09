using ClaimsManagamentSystem.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using System.IO;

namespace ClaimsManagementSystem.Controllers
{
    [HttpPost]
public ActionResult SubmitClaim(LecturerClaim claim, HttpPostedFileBase file)
{
    if (ModelState.IsValid)
    {
        // File handling logic
        if (file != null && file.ContentLength > 0)
        {
            string path = Path.Combine(Server.MapPath("~/Uploads/"), Path.GetFileName(file.FileName));
            file.SaveAs(path);
            claim.UploadedFilePath = path;
        }

        claim.Status = "Pending"; // Initial status
        claim.DateSubmitted = DateTime.Now;

        _context.LecturerClaims.Add(claim);
        _context.SaveChanges();

        return RedirectToAction("ClaimSubmitted");
    }
    return View(claim);
}

}

