using Moq;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Services;
using FluentAssertions;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Exceptions;

namespace VibeMoment.UnitTests;

public class PhotoServiceTests
{
    private readonly Mock<IPhotoRepository> _photoRepositoryMock;
    private readonly PhotoService _photoService;

    public PhotoServiceTests()
    {
        _photoRepositoryMock = new Mock<IPhotoRepository>();
        _photoService = new PhotoService(_photoRepositoryMock.Object);
    }
    [Fact]
    public async Task GetPhotoAsync_ShouldReturnPhotoById_WhenPhotoExists()
    {
        //Arrange
        var photoId = Guid.NewGuid();

        var expectedPhoto = new PhotoDto
        {
            Id = photoId,
        };

        _photoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(photoId))
            .ReturnsAsync(expectedPhoto);
        
        //Act
        var result = await _photoService.GetPhotoAsync(photoId);
        
        //Assert
        result.Should().BeEquivalentTo(expectedPhoto);
        
        _photoRepositoryMock.Verify(
            repo => repo.GetByIdAsync(photoId),
            Times.Once);
    }

    [Fact]
    public async Task GetPhotoAsync_ShouldThrowNotFoundException_WhenPhotoDoesntExists()
    {
        //Arrange
        var photoId = Guid.NewGuid();

        _photoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(photoId))
            .ReturnsAsync((PhotoDto?)null);
        
        //Act
        var act = async () => await _photoService.GetPhotoAsync(photoId);
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);

        _photoRepositoryMock.Verify(
            repo => repo.GetByIdAsync(photoId),
            Times.Once);
    }

    [Fact]
    public async Task UpdatePhotoAsync_ShouldUpdatePhoto_WhenDataIsValid()
    {
        //Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Test photo";

        var dto = new UpdatePhotoDto
        {
            Id = photoId,
            Title = title
        };

        var existingPhoto = new PhotoDto
        {
            Id = photoId,
            UserId = userId,
            Title = "Old title",
            AddedAt = DateTime.UtcNow.AddMinutes(-30)
        };

        var updatedPhoto = new PhotoDto
        {
            Id = photoId,
            UserId = userId,
            Title = title,
            AddedAt = existingPhoto.AddedAt,
            UpdatedAt = DateTime.UtcNow
        };
        
         _photoRepositoryMock
             .Setup(repo => repo.GetByIdAsync(photoId))
             .ReturnsAsync(existingPhoto);
         
         _photoRepositoryMock
             .Setup(repo => repo.UpdateAsync(It.IsAny<UpdatePhotoDto>()))
             .ReturnsAsync(updatedPhoto);
         
         //Act
         var result = await _photoService.UpdatePhotoAsync(dto, userId);
         
         //Assert 
         result.Should().NotBeNull();
         result.Id.Should().Be(photoId);
         result.UserId.Should().Be(userId);
         result.Title.Should().BeEquivalentTo(title);
         result.UpdatedAt.Should().NotBeNull();
         
         _photoRepositoryMock.Verify(
             repo => repo.GetByIdAsync(photoId),
             Times.Once);

         _photoRepositoryMock.Verify(
             repo => repo.UpdateAsync(It.IsAny<UpdatePhotoDto>()),
             Times.Once);
    }

    [Fact]
    public async Task UpdatePhotoAsync_ShouldThrowNotFoundException_WhenPhotoDoesntExist()
    {
        //Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var dto = new UpdatePhotoDto()
        {
            Id = photoId,
        };
        
        _photoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(photoId))
            .ReturnsAsync((PhotoDto?)null);
        
        //Act
        var act = async () => await _photoService.UpdatePhotoAsync(dto, userId);
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);

        _photoRepositoryMock.Verify(
            repo => repo.GetByIdAsync(photoId),
            Times.Once);

        _photoRepositoryMock.Verify(
            repo => repo.UpdateAsync(It.IsAny<UpdatePhotoDto>()),
            Times.Never);
    }

    [Fact]
    public async Task UpdatePhotoAsync_ShouldThrowForbiddenAccessException_WhenUserIdIsNotOwner()
    {
        //Arrange
        var photoId = Guid.NewGuid();
        var ownerUserId = Guid.NewGuid();
        var currentUserId = Guid.NewGuid();
        
        var dto = new UpdatePhotoDto
        {
            Id = photoId
        };

        var existingPhoto = new PhotoDto
        {
            Id = photoId,
            UserId = ownerUserId,
        };
        
        _photoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(photoId))
            .ReturnsAsync(existingPhoto);
        
        //Act
        var act = async () => await _photoService.UpdatePhotoAsync(dto, currentUserId);
        
        //Assert
        await Assert.ThrowsAsync<ForbiddenAccessException>(act);

        _photoRepositoryMock.Verify(
            repo => repo.GetByIdAsync(photoId),
            Times.Once);
        
        _photoRepositoryMock.Verify(
            repo => repo.UpdateAsync(It.IsAny<UpdatePhotoDto>()),
            Times.Never);
    }

    [Fact]
    public async Task UpdatePhotoAsync_ShouldThrowBusinessLogicException_WhenTimeExpired()
    {
        //Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var dto = new UpdatePhotoDto
        {
            Id = photoId
        };

        var existingPhoto = new PhotoDto
        {
            Id = photoId,
            UserId = userId,
            AddedAt = DateTime.UtcNow.AddHours(-2)
        };
        
        _photoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(photoId))
            .ReturnsAsync(existingPhoto);
        
        //Act
        var act = async () => await _photoService.UpdatePhotoAsync(dto, userId);
        
        //Assert
        await Assert.ThrowsAsync<BusinessLogicException>(act);
        
        _photoRepositoryMock.Verify(
            repo => repo.GetByIdAsync(photoId),
            Times.Once);
        
        _photoRepositoryMock.Verify(
            repo => repo.UpdateAsync(It.IsAny<UpdatePhotoDto>()),
            Times.Never);
        
    }

    [Fact]
    public async Task DeletePhoto_Async_ShouldDeletePhoto_WhenPhotoExists()
    {
        //Arrange
         var photoId = Guid.NewGuid();

         var existingPhoto = new PhotoDto
         {
             Id = photoId
         };

         _photoRepositoryMock
             .Setup(repo => repo.GetByIdAsync(photoId))
             .ReturnsAsync(existingPhoto);
         
         //Act
         await _photoService.DeletePhotoAsync(photoId);
         
         //Assert
         _photoRepositoryMock.Verify(
             repo => repo.GetByIdAsync(photoId),
             Times.Once);
         
         _photoRepositoryMock.Verify(
             repo => repo.DeleteAsync(photoId), 
             Times.Once);
    }
}