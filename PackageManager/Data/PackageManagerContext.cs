using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PackageManager.Models;

namespace PackageManager.Data
{
    public class PackageManagerContext : DbContext
    {
        public PackageManagerContext (DbContextOptions<PackageManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Package> Package { get; set; } = default!;
    }
}
