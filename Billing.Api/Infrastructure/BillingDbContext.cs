using Billing.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Billing.Api.Infrastructure;

public class BillingDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Order> Orders { get; set; }

	public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options) {}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Order>()
			.HasOne(u => u.User)
			.WithMany(o => o.Orders)
			.HasForeignKey(p => p.UserId);
	}
}

