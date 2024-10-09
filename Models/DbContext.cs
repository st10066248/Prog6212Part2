using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;

namespace ClaimsManagamentSystem.Models
{
    

    namespace ClaimsManagementSystem.Models
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext() : base("DefaultConnection") { }

            public DbSet<LecturerClaim> LecturerClaims { get; set; }
            // Add other DbSet properties as needed
        }
    }


}
