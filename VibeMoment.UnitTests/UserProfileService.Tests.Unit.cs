using FluentAssertions;
using Moq;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Services;

namespace VibeMoment.UnitTests;

public class UserProfileTest
{
    private readonly Mock<IUserProfileRepository> _userProfileRepositoryMock; 
    private readonly UserProfileService _userProfileService;

    public UserProfileTest()
    {
        _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
        _userProfileService = new UserProfileService(_userProfileRepositoryMock.Object);
    }

    [Fact]
    public async Task GetProfileAsync_ShouldReturnUserProfile_WhenUserExists()
    {
        //Arrange
        var userId = Guid.NewGuid();

        var expectedUserId = new UserProfileDto
        {
            Id = userId
        };

        _userProfileRepositoryMock
            .Setup(repo => repo.GetByUserIdAsync(userId))
            .ReturnsAsync(expectedUserId);
        
        //Act
        var result = await _userProfileService.GetProfileAsync(userId);
        
        //Assert
        result.Should().BeEquivalentTo(expectedUserId);
        
        _userProfileRepositoryMock.Verify(
            repo => repo.GetByUserIdAsync(userId),
            Times.Once);
    }

    [Fact]
    public async Task GetProfileAsync_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        //Arrange
        var userId = Guid.NewGuid();
        
        _userProfileRepositoryMock
            .Setup(repo => repo.GetByUserIdAsync(userId))
            .ReturnsAsync((UserProfileDto)null);
        
        //Act
        var act = async () => await _userProfileService.GetProfileAsync(userId);
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        
        _userProfileRepositoryMock.Verify(
            repo => repo.GetByUserIdAsync(userId),
            Times.Once);
    }
}