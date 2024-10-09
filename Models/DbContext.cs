using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClaimsManagamentSystem.Models
{
    public class ClaimsDbContext : DbContext
    {
        public DbSet<LecturerClaim> LecturerClaims { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
