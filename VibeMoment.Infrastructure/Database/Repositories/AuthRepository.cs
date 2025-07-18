using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Security;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    public AuthRepository(AppDbContext context, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _context = context;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> CreateUserAsync(RegisterDto dto)
    {
        _passwordHasher.CreatePasswordHash(dto.Password, out var passwordHash, out var passwordSalt);
        
        var user = _mapper.Map<User>(dto);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        
      _context.Users.Add(user); 
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<Guid?> GetValidUserIdAsync(string usernameOrEmail, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u =>
                (u.Email == usernameOrEmail || u.UserName == usernameOrEmail));
        
        if(user == null || !_passwordHasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            return null;

        return user?.Id;
    }
}