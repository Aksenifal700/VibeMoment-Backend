using FluentAssertions;
using Moq;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;
using VibeMoment.BusinessLogic.Services;

namespace VibeMoment.UnitTests;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly AuthService _authService;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGenerator;
    private readonly Mock<IRefreshTokenService> _refreshTokenService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtTokenGenerator = new Mock<IJwtTokenGenerator>();
        _refreshTokenService = new Mock<IRefreshTokenService>();
        
        _authService = new AuthService(_userRepositoryMock.Object, 
            _jwtTokenGenerator.Object,
            _refreshTokenService.Object);
    }

    [Fact]
    public async Task SignInAsync_ShouldReturnSignedUp_WhenUserExists()
    {
        //Arrange
        var usernameOrEmail = "aleksander228@gmail.com";
        var password = "password3!";
        var jwtToken = "2132141235464574891789127489";
        var refreshToken = "1323124161975712313832";
        var userId = Guid.NewGuid();

        var refreshTokenDto = new RefreshTokenDto
        {
            Token = refreshToken,
        };
        
        var dto = new SignInResultDto
        {
          Token = jwtToken,
          RefreshToken = refreshToken
        };

        var signinDto = new SigninDto
        {
           UsernameOrEmail = usernameOrEmail,
           Password = password
        };
        
        _userRepositoryMock
            .Setup(repo => repo.GetValidUserIdAsync(usernameOrEmail, password))
            .ReturnsAsync(userId);

        _jwtTokenGenerator
            .Setup(repo => repo.GenerateToken(It.IsAny<TokenGenerationDto>()))
            .Returns(jwtToken);
        
        _refreshTokenService
            .Setup(repo => repo.GenerateAndSaveAsync(userId))
            .ReturnsAsync(refreshTokenDto);
        
        //Act
        var result = await _authService.SignInAsync(signinDto);
        
        //Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(jwtToken);
        result.RefreshToken.Should().Be(refreshToken);
        
        _userRepositoryMock.Verify(
            repo => repo.GetValidUserIdAsync(usernameOrEmail, password),
            Times.Once);
        
        _jwtTokenGenerator.Verify(
            repo => repo.GenerateToken(It.IsAny<TokenGenerationDto>()),
            Times.Once);

        _refreshTokenService.Verify(
            repo => repo.GenerateAndSaveAsync(userId),
            Times.Once);

    }

    [Fact]
    public async Task SignInAsync_ShouldThrowUserNotFoundException_WhenUserDoesNotExist()
    {
        //Arrange
        var usernameOrEmail = "aleksander228@gmail.com";
        var password = "password3!";

        var signinDto = new SigninDto
        {
            UsernameOrEmail = usernameOrEmail,
            Password = password
        };
        
        _userRepositoryMock
            .Setup(repo => repo.GetValidUserIdAsync(usernameOrEmail, password))
            .ReturnsAsync((Guid?)null);
        
        //Act
        var act = async () => await _authService.SignInAsync(signinDto);
        
        //Assert
        await Assert.ThrowsAsync<UserNotFoundException>(act);
        
        _userRepositoryMock.Verify(
            repo => repo.GetValidUserIdAsync(usernameOrEmail, password),
            Times.Once);
    }
}