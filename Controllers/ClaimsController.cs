using System.IO;
using System.Web.Mvc;
using ClaimsManagamentSystem.Models.ClaimsManagementSystem.Models;
using ClaimsManagementSystem.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsManagementSystem.Controllers
{
    public class ClaimsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Claims/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LecturerClaim claim)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (claim.SupportingDocument != null && claim.SupportingDocument.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(claim.SupportingDocument.FileName);
                    var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    claim.SupportingDocument.SaveAs(path);
                    claim.SupportingDocumentPath = "/UploadedFiles/" + fileName; // Store the file path
                }

                // Save the claim to the database
                db.LecturerClaims.Add(claim);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(claim);
        }
        // GET: Claims
        public ActionResult Index()
        {
            var claims = db.LecturerClaims.ToList();
            return View(claims);
        }

    }


}




