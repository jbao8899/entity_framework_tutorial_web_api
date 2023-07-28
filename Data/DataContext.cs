using entity_framework_tutorial_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework_tutorial_web_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Contact> ContactDbSet { get; set; }
    }
}

// data source=LTUS156423\MSSQLSERVER3;initial catalog=master;trusted_connection=true
