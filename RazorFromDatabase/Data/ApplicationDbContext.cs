using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorFromDatabase.Models;

namespace RazorFromDatabase.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Page> Pages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Page>()
                .ToTable("Core_Page");
        }
    }
}
