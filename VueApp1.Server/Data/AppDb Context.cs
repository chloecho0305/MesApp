using Microsoft.EntityFrameworkCore;
using VueApp1.Server.Models;

namespace VueApp1.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<ProcessRecord> ProcessRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessRecord>()
                 .HasOne(pr => pr.WorkOrder)
                 .WithMany(wo => wo.ProcessRecords)
                 .HasForeignKey(pr => pr.WorkOrderId);
            // 其他模型配置可以在這裡添加
            base.OnModelCreating(modelBuilder);
        }
    }
}
