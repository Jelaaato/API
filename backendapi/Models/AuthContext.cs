using backendapi.EntityClasses;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace backendapi.Models
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("APIUsers");
            modelBuilder.Entity<IdentityRole>().ToTable("APIRoles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("APIUserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("APIUserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("APIUserLogin");
        }
    }
}