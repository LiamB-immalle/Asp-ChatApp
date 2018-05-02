using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace test.Models
{
    public class BerichtContext : DbContext
    {
        public BerichtContext (DbContextOptions<BerichtContext> options)
            : base(options)
        {
        }

        public DbSet<test.Models.Bericht> Bericht { get; set; }
    }
}
