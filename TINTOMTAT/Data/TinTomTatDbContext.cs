using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TINTOMTAT.Data.Entites;

namespace TINTOMTAT.Data
{
    public class TinTomTatDbContext : DbContext
    {
        public TinTomTatDbContext() : base("tintomtatBdConnectstring")
        {
        }       


        public DbSet<DanhMucBaiViet> DanhMucBaiViets { get; set; }
        public DbSet<BaiViet> BaiViets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}