using DistanceCalculator.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace DistanceCalculator.Infrastructure.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<CalculationEntry> CalculationEntries { get; set; }
    }
}