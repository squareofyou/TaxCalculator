using Microsoft.EntityFrameworkCore;
using TaxCalculator.Models;

namespace TaxCalculator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Municipality> municipalities { get; set; }
        public DbSet<TaxType> taxTypes { get; set; }

    }
}
