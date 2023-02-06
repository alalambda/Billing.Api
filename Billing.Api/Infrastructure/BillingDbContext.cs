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

		modelBuilder.Entity<User>().HasData(
			new User
			{
				UserId = 1,
				FirstName = "John1",
				LastName = "Doe1"
			},
            new User
            {
                UserId = 2,
                FirstName = "John2",
                LastName = "Doe2"
            },
            new User
            {
                UserId = 3,
                FirstName = "John3",
                LastName = "Doe3"
            }
        );

		modelBuilder.Entity<Order>().HasData(
			new Order
			{
                OrderNumber = 1,
                UserId = 3,
				PayableAmount = 100,
				PaymentGateway = Models.Enums.PaymentGateway.Klarna,
				Description = "Oh no, I have to pay",
				OrderStatus = Models.Enums.OrderStatus.AwaitingPayment
			},
            new Order
            {
                OrderNumber = 2,
                UserId = 3,
                PayableAmount = 50,
                PaymentGateway = Models.Enums.PaymentGateway.Adyen,
                Description = "I already ordered everything",
                OrderStatus = Models.Enums.OrderStatus.Completed
            },
            new Order
            {   
                OrderNumber = 3,
                UserId = 3,
                PayableAmount = 500,
                PaymentGateway = Models.Enums.PaymentGateway.AmazonPay,
                Description = "I didn't like what I ordered",
                OrderStatus = Models.Enums.OrderStatus.Refunded
            }
        );
	}
}

