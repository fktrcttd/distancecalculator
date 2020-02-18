using System.Collections.Generic;
using System.IO;
using DistanceCalculator.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DistanceCalculator.Infrastructure.Data
{
    public class DataContext: DbContext
    {
        public DataContext()
        {
            
        }
        
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<CalculationEntry> CalculationEntries { get; set; }
    }
    
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext> 
    { 
        public DataContext CreateDbContext(string[] args) 
        { 
            string json = File.ReadAllText("./conf.json");
            var config = JsonConvert.DeserializeObject<Configuration>(json);
            var builder = new DbContextOptionsBuilder<DataContext>(); 
            builder.UseNpgsql(config.DatabaseConnection); 
            return new DataContext(builder.Options); 
        } 
    }
}