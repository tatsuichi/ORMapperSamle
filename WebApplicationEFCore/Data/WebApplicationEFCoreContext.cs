using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplicationEFCore.Models;

namespace WebApplicationEFCore.Data
{
    public class WebApplicationEFCoreContext : DbContext
    {
        public WebApplicationEFCoreContext (DbContextOptions<WebApplicationEFCoreContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplicationEFCore.Models.Student> Student { get; set; } = default!;
    }
}
