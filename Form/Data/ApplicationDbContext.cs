using Form.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Form.Data
{
   
    
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions options) : base(options) { }

            public DbSet<Student> Students { get; set; }
        }
}
﻿