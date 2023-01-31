using Billing.Api.Models.Entities;

namespace Billing.Api.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private BillingDbContext _dbContext;

	public UserRepository(BillingDbContext dbContext)
	{
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
	}

    public async Task<User?> GetUserAsync(int userId) => await _dbContext.Users.FindAsync(userId);
}

