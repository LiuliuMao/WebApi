using IRepository;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SqlServerDBContext : DbContext, ISqlServerDBContext
    {
        public SqlServerDBContext()
        {

        }
        public SqlServerDBContext(DbContextOptions<SqlServerDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<CompanyInfo> CompanyInfo { get; set; }
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        public virtual DbSet<MenuInfo> MenuInfo { get; set; }
    }
}
