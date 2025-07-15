using Microsoft.EntityFrameworkCore;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateUserAsync(string email, string password, string username)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            UserName = username,
            PasswordHash = password
        };

      _context.Users.Add(user); 
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<Guid?> GetValidUserIdAsync(string usernameOrEmail, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u =>
                (u.Email == usernameOrEmail || u.UserName == usernameOrEmail) &&
                u.PasswordHash == password);

        return user?.Id;
    }
    
    public Task SignOutAsync()
    {
        return Task.CompletedTask;
    }
}