using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Moq;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Services;

namespace VibeMoment.UnitTests;

public class RefreshTokenTest
{
    private readonly RefreshTokenService _refreshService;
    private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public RefreshTokenTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();
        _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

        _refreshService = new RefreshTokenService(_refreshTokenRepositoryMock.Object,
            _jwtTokenGeneratorMock.Object,
            _userRepositoryMock.Object);
    }

    [Fact]
    public async Task GenerateAndSaveAsync_ShouldCreateRefreshToken_WhenUserIdIsValid()
    {
        //Arrange
        var userId = Guid.NewGuid();
        
        var dto = new RefreshTokenDto()
        {
            UserId = userId,
            Token = Guid.NewGuid().ToString(),
            ExpiresOnUtc = DateTime.UtcNow.AddHours(1)
        };

        _refreshTokenRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<CreateRefreshTokenDto>()))
            .ReturnsAsync(dto);
        
        //Act
        var result = await _refreshService.GenerateAndSaveAsync(userId);
        
        //Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(dto.Token);
        result.UserId.Should().Be(dto.UserId);
        
        _refreshTokenRepositoryMock.Verify(
            repo => repo.CreateAsync(It.IsAny<CreateRefreshTokenDto>()), 
            Times.Once);
    }

    [Fact]
    public async Task RefreshJwtAsync_ShouldRecreateJwtToken_WhenRefreshTokenIsValid()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var jwtToken = Guid.NewGuid().ToString();
        var refreshToken = Guid.NewGuid().ToString();

        var tokenDto = new RefreshTokenDto()
        {
            UserId = userId,
            Token = refreshToken,
            ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
            IsRevoked = false,
        };
        
        var userDto = new UserDto()
        {
           Id = userId,
           Email = "test@gmail.com"
        };
        
        _refreshTokenRepositoryMock
            .Setup(repo => repo.GetRefreshTokenAsync(refreshToken))
            .ReturnsAsync(tokenDto);
        
        _userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync(userDto);
        
        _jwtTokenGeneratorMock
            .Setup(repo => repo.GenerateToken(It.IsAny<TokenGenerationDto>()))
            .Returns(jwtToken);
        
        //Act
        var result = await _refreshService.RefreshJwtAsync(refreshToken);
        
        //Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(jwtToken);
        result.RefreshToken.Should().Be(refreshToken);
        
        _refreshTokenRepositoryMock.Verify(
            repo => repo.GetRefreshTokenAsync(refreshToken), 
            Times.Once);

        _userRepositoryMock.Verify(
            repo => repo.GetByIdAsync(userId),
            Times.Once);
            
        _jwtTokenGeneratorMock.Verify(
            repo => repo.GenerateToken(It.IsAny<TokenGenerationDto>()),
            Times.Once);
    }

    [Fact]
    public async Task RefreshJwtAsync_ShouldThrowInvalidRefreshTokenException_WhenRefreshTokenDoesNotExist()
    {
        //Arrange
        var refreshToken = Guid.NewGuid().ToString();
        
        _refreshTokenRepositoryMock
            .Setup(repo => repo.GetRefreshTokenAsync(refreshToken))
            .ReturnsAsync((RefreshTokenDto?)null);
        
        //Act
        var act = async () => await _refreshService.RefreshJwtAsync(refreshToken);
        
        //Assert
        await Assert.ThrowsAsync<InvalidRefreshTokenException>(act);
        
        _refreshTokenRepositoryMock.Verify(
            repo => repo.GetRefreshTokenAsync(refreshToken),
            Times.Once);
        
        _userRepositoryMock.Verify(
            repo => repo.GetByIdAsync(It.IsAny<Guid>()),
            Times.Never);
            
        _jwtTokenGeneratorMock.Verify(
            repo => repo.GenerateToken(It.IsAny<TokenGenerationDto>()),
            Times.Never);
    }

    [Fact]
    public async Task RefreshJwtAsync_ShouldThrowInvalidRefreshTokenException_WhenRefreshTokenIsExpired()
    {
        //Arrange
        var refreshToken = Guid.NewGuid().ToString();
        var userId = Guid.NewGuid();

        var tokenDto = new RefreshTokenDto()
        {
            UserId = userId,
            Token = refreshToken,
            ExpiresOnUtc = DateTime.UtcNow.AddHours(-1),
            IsRevoked = false
        };
        
        _refreshTokenRepositoryMock
            .Setup(repo => repo.GetRefreshTokenAsync(refreshToken))
            .ReturnsAsync(tokenDto);
        
        //Act
        var act = async () => await _refreshService.RefreshJwtAsync(refreshToken);
        
        //Assert
        await Assert.ThrowsAsync<InvalidRefreshTokenException>(act);
        
        _refreshTokenRepositoryMock.Verify(
            repo => repo.GetRefreshTokenAsync(refreshToken),
            Times.Once);
        
        _userRepositoryMock.Verify(
            repo => repo.GetByIdAsync(userId),
            Times.Never);
        
        _jwtTokenGeneratorMock.Verify(
            repo => repo.GenerateToken(It.IsAny<TokenGenerationDto>()),
            Times.Never);
    }

    [Fact]
    public async Task RefreshJwtAsync_ShouldThrowInvalidRefreshTokenException_WhenRefreshTokenIsRevoked()
    {
        //Arrange
        var refreshToken = Guid.NewGuid().ToString();
        var userId = Guid.NewGuid();

        var tokenDto = new RefreshTokenDto()
        {
            UserId = userId,
            Token = refreshToken,
            ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
            IsRevoked = true
        };
        
        _refreshTokenRepositoryMock
            .Setup(repo => repo.GetRefreshTokenAsync(refreshToken))
            .ReturnsAsync(tokenDto);
        
        //Act
        var act = async () => await _refreshService.RefreshJwtAsync(refreshToken);
        
        //Assert
        await Assert.ThrowsAsync<InvalidRefreshTokenException>(act);
        
        _refreshTokenRepositoryMock.Verify(
            repo => repo.GetRefreshTokenAsync(refreshToken),
            Times.Once);
        
        _userRepositoryMock.Verify(
            repo => repo.GetByIdAsync(userId),
            Times.Never);
        
        _jwtTokenGeneratorMock.Verify(
            repo => repo.GenerateToken(It.IsAny<TokenGenerationDto>()),
            Times.Never);
    }

    [Fact]
    public async Task RefreshJwtAsync_ShouldThrowUserNotFoundException_WhenUserDoesNotExist()
    {
        //Arrange
        var refreshToken = Guid.NewGuid().ToString();
        var userId = Guid.NewGuid();
        
        var tokenDto = new RefreshTokenDto()
        {
            UserId = userId,
            Token = refreshToken,
            ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
            IsRevoked = false
        };
        
        _refreshTokenRepositoryMock
            .Setup(repo => repo.GetRefreshTokenAsync(refreshToken))
            .ReturnsAsync(tokenDto);
        
        _userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync((UserDto?)null);
        
        //Act
        var act = async () => await _refreshService.RefreshJwtAsync(refreshToken);
        
        //Assert
        await Assert.ThrowsAsync<UserNotFoundException>(act);
        
        _refreshTokenRepositoryMock.Verify(
            repo => repo.GetRefreshTokenAsync(refreshToken),
            Times.Once);
        
        _userRepositoryMock.Verify(
            repo => repo.GetByIdAsync(userId),
            Times.Once);
        
        _jwtTokenGeneratorMock.Verify(
            repo => repo.GenerateToken(It.IsAny<TokenGenerationDto>()),
            Times.Never);
    }

    [Fact]
    public async Task RevokeAsync_ShouldReturnTrue_WhenRefreshTokenIsRevoked()
    {
        //Arrange
        var token = Guid.NewGuid().ToString();

        var dto = new RefreshTokenDto()
        {
            Token = token,
            ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
            IsRevoked = false
        };
        
        _refreshTokenRepositoryMock
            .Setup(repo => repo.GetRefreshTokenAsync(token))
            .ReturnsAsync(dto);
        
        //Act
        await _refreshService.RevokeAsync(token);
        
        //Assert
        _refreshTokenRepositoryMock.Verify(
            repo => repo.GetRefreshTokenAsync(token),
            Times.Once);
        
        _refreshTokenRepositoryMock.Verify(
            repo => repo.RevokeAsync(token),
            Times.Once);
    }

    [Fact]
    public async Task RevokeAsync_ShouldReturnInvalidRefreshTokenException_WhenRefreshTokenIsNotRevoked()
    {
        //Arrange
        var token = Guid.NewGuid().ToString();

        var dto = new RefreshTokenDto()
        {
            Token = token,
            ExpiresOnUtc = DateTime.UtcNow.AddHours(1),
            IsRevoked = true
        };
        
        _refreshTokenRepositoryMock
            .Setup(repo => repo.GetRefreshTokenAsync(token))
            .ReturnsAsync(dto);
        
        //Act
        var act = async () => await _refreshService.RevokeAsync(token);
        
        //Assert
        await Assert.ThrowsAsync<InvalidRefreshTokenException>(act);
        
        _refreshTokenRepositoryMock.Verify(
            repo => repo.GetRefreshTokenAsync(token),
            Times.Once);
        
        _refreshTokenRepositoryMock.Verify(
            repo => repo.RevokeAsync(token),
            Times.Never);
    }
     
}