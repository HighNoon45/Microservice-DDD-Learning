using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Margin> Margins { get; set; }
        public DbSet<Markup> Markups { get; set; }
        public DbSet<Pricing> Pricings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DENI4002\SQLEXPRESS;Database=EFCoreMap; Integrated Security=True; TrustServerCertificate=True;");
            //optionsBuilder.UseSqlite("Data Source=InMemorySample;Mode=Memory;Cache=Shared");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Pricing>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Cost>()
                .HasQueryFilter(x => !x.IsDeleted)
                .HasOne<Pricing>()
                .WithMany(x => x.Costs)
                .HasForeignKey(x => x.PricingId) ;

            modelBuilder.Entity<Margin>()
                .HasQueryFilter(x => !x.IsDeleted)
                .HasOne<Pricing>()
                .WithMany(x => x.Margins)
                .HasForeignKey(x => x.PricingId);
            
            modelBuilder.Entity<Markup>()
                .HasQueryFilter(x => !x.IsDeleted)
                .HasOne<Pricing>()
                .WithMany(x => x.Markups)
                .HasForeignKey(x => x.PricingId);
        }
    }
}
