using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevators_Customer_Portal.Areas.Identity.Data;

namespace Rocket_Elevators_Customer_Portal.Data
{
    public class Rocket_Elevators_Customer_PortalContext : IdentityDbContext<Rocket_Elevators_Customer_PortalUser>
    {
        public Rocket_Elevators_Customer_PortalContext(DbContextOptions<Rocket_Elevators_Customer_PortalContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
