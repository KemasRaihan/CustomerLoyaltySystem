using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoyaltySoftware.Models;

namespace LoyaltySoftware.Data
{
    public class LoyaltySoftwareContext : DbContext
    {
        public LoyaltySoftwareContext (DbContextOptions<LoyaltySoftwareContext> options)
            : base(options)
        {
        }

        public DbSet<LoyaltySoftware.Models.Userdbo> User { get; set; }
    }
}
