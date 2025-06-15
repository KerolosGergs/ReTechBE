using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReTechApi.Models;

namespace ReTechApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RecyclingRequest> RecyclingRequests { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configure RecyclingRequest entity
            modelBuilder.Entity<RecyclingRequest>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecyclingRequest>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(r => r.AssignedCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Reward entity
            modelBuilder.Entity<Reward>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reward>()
                .HasOne<RecyclingRequest>()
                .WithMany()
                .HasForeignKey(r => r.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasPrecision(18, 2);

            modelBuilder.Entity<RecyclingRequest>()
                .Property(r => r.EstimatedValue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reward>()
                .Property(r => r.Points)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reward>()
                .Property(r => r.Value)
                .HasPrecision(18, 2);

            modelBuilder.Entity<RecyclingRequest>().HasData(
new RecyclingRequest
{
    Id = "REQ1",
    CustomerId = "90a55d4a-547c-435e-a033-e369092bb87c",
    AssignedCompanyId = null,
    ItemDescription = "Used iPhone 12 with minor scratches",
    Location = "Cairo",
    EstimatedValue = 500,
    Status = RequestStatus.Pending,
    RequestDate = new DateTime(2024, 6, 10),
    DeviceType = "Smartphone",
    DeviceModel = "iPhone 12",
    DeviceCondition = "Good",
    Description = "Some scratches on the back",
    ImageUrl = "/images/recycling/iphone12.jpg",
    CreatedAt = new DateTime(2025, 6, 10),
    PickupAddress = "Downtown Branch"
},
new RecyclingRequest
{
    Id = "REQ2",
    CustomerId = "90a55d4a-547c-435e-a033-e369092bb87c",
    AssignedCompanyId = "a21c50ad-4b69-4b0f-bd61-18d11425d910",
    ItemDescription = "MacBook Pro with battery issues",
    Location = "Giza",
    EstimatedValue = 800,
    Status = RequestStatus.Assigned,
    RequestDate = new DateTime(2025, 6, 12),
    DeviceType = "Laptop",
    DeviceModel = "MacBook Pro 2019",
    DeviceCondition = "Fair",
    Description = "Needs battery replacement",
    ImageUrl = "/images/recycling/macbook.jpg",
    CreatedAt = new DateTime(2024, 6, 12),
    PickupDate = new DateTime(2024, 6, 18),
    PickupAddress = "6th October Branch"
}
);

        }

    }
    }



