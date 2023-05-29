using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kalkulator_Kalorii.MVVM.Model
{
    public class HistoriaTabela : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Historia> Histories { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=Kalkulator.db");
        }
    }
}
