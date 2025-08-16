using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database.Repositories;
 
public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly  AppDbContext _context;
    private readonly  IMapper _mapper;

    public RefreshTokenRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RefreshTokenDto> CreateAsync(CreateRefreshTokenDto dto)
    {
        var refreshToken = _mapper.Map<RefreshToken>(dto);
        
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<RefreshTokenDto>(refreshToken);
    }

    public async Task<RefreshTokenDto?> GetRefreshTokenAsync(string token)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(r => r.Token == token);

        return refreshToken is null
            ? null
            : _mapper.Map<RefreshTokenDto>(refreshToken);
    }

    public async Task RevokeAsync(string token)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(r => r.Token == token);
        
        if (refreshToken is null)
            return;
        
        refreshToken.IsRevoked = true;
        await _context.SaveChangesAsync();
    }
}