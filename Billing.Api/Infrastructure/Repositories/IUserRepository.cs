using Billing.Api.Models.Entities;

namespace Billing.Api.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(int userId);
}

