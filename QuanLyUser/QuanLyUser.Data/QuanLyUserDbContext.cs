using QuanLyUser.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyUser.Data
{
    public class QuanLyUserDbContext: DbContext
    {
        public QuanLyUserDbContext(): base("QuanLyUserConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<User> Users { set; get; }
        public DbSet<TradeInfomation> TradeInfomations { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
