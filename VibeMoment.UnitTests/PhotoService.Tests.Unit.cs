using Moq;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.UnitTests;

public class PhotoServiceTests
{
    private readonly Mock<IPhotoRepository> _photoRepositoryMock;
    private readonly PhotoService _photoService;
    private readonly Mock<IFileStorageService> _fileStorageService;
    private readonly Mock<ILogger<PhotoService>> _logger;

    public PhotoServiceTests()
    {
        _photoService = new PhotoService(
            _photoRepositoryMock.Object,
            _fileStorageService.Object,
            _logger.Object);
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
    public async Task DeletePhotoAsync_ShouldDeletePhoto_WhenPhotoExists()
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

    [Fact]
    public async Task UploadPhotoAsync_ShouldUploadPhoto_WhenPhotoUploaded()
    {
        // Arrange
        byte[] data = [1, 2, 3];
        using var stream = new MemoryStream(data);

        const string contentType = "image/jpeg";
        const string blobName = "photos-test";

        var uploadDto = new UploadPhotoDto
        {
            Title = "Test photo",
            UserId = Guid.NewGuid(),
            Data = []
        };

        var expectedPhoto = new PhotoDto
        {
            Id = Guid.NewGuid(),
            Title = uploadDto.Title,
            UserId = uploadDto.UserId,
            BlobName = blobName,
            Data = []
        };

        _fileStorageService
            .Setup(s => s.UploadAsync(
                stream,
                It.IsAny<string>(),
                contentType,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(blobName);

        _photoRepositoryMock
            .Setup(r => r.SaveAsync(It.Is<UploadPhotoDto>(dto =>
                dto.Title == uploadDto.Title &&
                dto.UserId == uploadDto.UserId &&
                dto.BlobName == blobName &&
                dto.Data.Length == 0)))
            .ReturnsAsync(expectedPhoto);

        // Act
        var result = await _photoService.UploadPhotoAsync(
            uploadDto,
            stream,
            contentType);

        // Assert
        Assert.Same(expectedPhoto, result);

        _fileStorageService.Verify(s => s.UploadAsync(
            stream,
            It.IsAny<string>(),
            contentType,
            It.IsAny<CancellationToken>()), Times.Once);

        _photoRepositoryMock.Verify(r => r.SaveAsync(
            It.Is<UploadPhotoDto>(dto =>
                dto.Title == uploadDto.Title &&
                dto.UserId == uploadDto.UserId &&
                dto.BlobName == blobName &&
                dto.Data.Length == 0)), Times.Once);

        _fileStorageService.Verify(s => s.DeleteAsync(
            It.IsAny<string>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact] 
    public async Task UploadPhotoAsync_ShouldThrow_WhenStorageUploadFails()
    {
        // Arrange
        byte[] data = [1, 2, 3];
        using var stream = new MemoryStream(data);

        const string contentType = "image/jpeg";

        var uploadDto = new UploadPhotoDto
        {
            Title = "Test photo",
            UserId = Guid.NewGuid(),
            Data = []
        };

        var uploadException = new Exception("storage upload failed");
        
        _fileStorageService.Setup(s => s.UploadAsync(
            stream,
            It.IsAny<string>(),
            contentType,
            It.IsAny<CancellationToken>()))
            .ThrowsAsync(uploadException);
        
       
        //Act
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _photoService.UploadPhotoAsync(uploadDto, stream, contentType));
        //Assert
        _photoRepositoryMock.Verify(
            r => r.SaveAsync(It.IsAny<UploadPhotoDto>()),
            Times.Never);

        _fileStorageService.Verify(
            s => s.DeleteAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
        
        Assert.Same(uploadException, exception);
    }

    [Fact]
    public async Task UploadPhotoAsync_ShouldDeleteBlob_WhenSaveFails()
    {
        //Arrange
        byte[] data = [1, 2, 3];
        using var stream = new MemoryStream(data);
        
        const string contentType = "image/jpeg";
        const string blobName = "photos-test";

        var uploadDto = new UploadPhotoDto()
        {
            Title = "Test photo",
            UserId = Guid.NewGuid(),
            Data = []
        };
        
        var saveException = new Exception("save upload failed");
        
        _photoRepositoryMock.Setup(r => r.SaveAsync(
            It.IsAny<UploadPhotoDto>())).ThrowsAsync(saveException);

        _fileStorageService
            .Setup(s => s.UploadAsync(
                stream,
                It.IsAny<string>(),
                contentType,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(blobName);
        
        _fileStorageService
            .Setup(s => s.DeleteAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        //Act
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _photoService.UploadPhotoAsync(uploadDto, stream, contentType));
        
        //Assert
        _fileStorageService.Verify(
            s => s.DeleteAsync(
                blobName,
                It.IsAny<CancellationToken>()),
            Times.Once);
        
        Assert.Same(saveException, exception);
        
    }
 
    [Fact]
    public async Task UploadPhotoAsync_ShouldPreserveSaveException_WhenCleanupFails()
    {
        //Arrange
        byte[] data = [1, 2, 3];
        using var stream = new MemoryStream(data);
        
        const string contentType = "image/jpeg";
        const string blobName = "photos-test";

        var uploadDto = new UploadPhotoDto()
        {
            Title = "Test photo",
            UserId = Guid.NewGuid(),
            Data = []
        };
        
        var saveException = new Exception("save upload failed");
        var cleanupException = new Exception("cleanup upload failed");
        
        
        _fileStorageService
            .Setup(s => s.UploadAsync(
                stream,
                It.IsAny<string>(),
                contentType,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(blobName);
        
      
    _photoRepositoryMock
        .Setup(r => r.SaveAsync(It.IsAny<UploadPhotoDto>()))
        .ThrowsAsync(saveException);

    _fileStorageService
        .Setup(s => s.DeleteAsync(
            blobName,
            It.IsAny<CancellationToken>()))
        .ThrowsAsync(cleanupException);

    // Act
    var exception = await Assert.ThrowsAsync<Exception>(() =>
        _photoService.UploadPhotoAsync(uploadDto, stream, contentType));

    // Assert
    Assert.Same(saveException, exception);

    _fileStorageService.Verify(
        s => s.DeleteAsync(
            blobName,
            It.IsAny<CancellationToken>()),
        Times.Once);

    _logger.Verify(
        logger => logger.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((state, _) =>
                state.ToString()!.Contains(blobName)),
            cleanupException,
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
        Times.Once);
    }
}