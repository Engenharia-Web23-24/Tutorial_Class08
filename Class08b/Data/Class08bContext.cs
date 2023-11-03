using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Class08b.Models;

namespace Class08b.Data
{
    public class Class08bContext : DbContext
    {
        public Class08bContext (DbContextOptions<Class08bContext> options)
            : base(options)
        {
        }

        public DbSet<Class08b.Models.User> User { get; set; } = default!;
    }
}
