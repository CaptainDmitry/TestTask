using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Currency> currencies { get; set; }
        public DbSet<CurrencyRate> currencyRates { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

    }
}
