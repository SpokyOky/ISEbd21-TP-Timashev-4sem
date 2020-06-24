using Microsoft.EntityFrameworkCore;
using SecuritySystemDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemDatabaseImplement
{
    public class SecuritySystemDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=SecuritySystemDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Raw> Raws { set; get; }
        public virtual DbSet<Equipment> Equipments { set; get; }
        public virtual DbSet<EquipmentRaw> EquipmentRaws { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
    }
}
