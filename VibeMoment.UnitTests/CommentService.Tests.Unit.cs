using FluentAssertions;
using Moq;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Services;

namespace VibeMoment.UnitTests;

public class CommentServiceTest
{
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly CommentService _commentService;
    private readonly Mock<IPhotoRepository> _photoRepositoryMock;

    public CommentServiceTest()
    {
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _photoRepositoryMock = new Mock<IPhotoRepository>();
        _commentService = new CommentService(_commentRepositoryMock.Object, _photoRepositoryMock.Object);
    }

    [Fact]
    public async Task AddCommentAsync_ShouldReturnComment_WhenCommentCreated()
    {
        //Arrange
        var commentId = Guid.NewGuid();
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var content = "Text comment";

        var addCommentDto = new AddCommentDto
        {
            PhotoId = photoId,
            Content = content,
            UserId = userId
        };

        var createdComment = new CommentDto
        {
            Id = commentId,
            UserId = userId,
            Content = content,
            PhotoId = photoId,
            CreatedAt = DateTime.Now,
        };
        
        _photoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(photoId))
            .ReturnsAsync(new PhotoDto { Id = photoId, Title = "Test photo" });
        
        _commentRepositoryMock
            .Setup(repo => repo.AddAsync(addCommentDto))
            .ReturnsAsync(createdComment);
        
        //Act
        var result = await _commentService.AddCommentAsync(addCommentDto);
        
        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(commentId);
        result.PhotoId.Should().Be(photoId);
        result.UserId.Should().Be(userId);
        result.Content.Should().Be(content);
        
        _commentRepositoryMock.Verify(
            repo => repo.AddAsync(addCommentDto),
            Times.Once);

    }

    [Fact]
    public async Task AddCommentAsync_ShouldThrowNotFoundException_WhenPhotoDoesNotExist()
    {
        //Arrange
        var dto = new AddCommentDto
        {
            PhotoId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Content = "Text comment"
        };

        _photoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(dto.PhotoId))
            .ReturnsAsync((PhotoDto?)null);

        //Act
        await Assert.ThrowsAsync<NotFoundException>(
            () => _commentService.AddCommentAsync(dto));

        //Assert
        _commentRepositoryMock.Verify(
            repo => repo.AddAsync(It.IsAny<AddCommentDto>()),
            Times.Never);
    }
    [Fact]
    public async Task DeleteCommentAsync_ShouldDeleteComent_WhenCommentExistsAndUserIsOwner()
    {
        //Arrange
        var commentId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();

        var existingComment = new CommentDto
        {
            Id = commentId,
            UserId = requestingUserId,
        };
        
        _commentRepositoryMock
            .Setup(repo => repo.GetByIdAsync(commentId))
            .ReturnsAsync(existingComment);
        
        //Act
        await _commentService.DeleteCommentAsync(commentId,requestingUserId);
        
        //Assert
        _commentRepositoryMock.Verify(
            repo => repo.GetByIdAsync(commentId),
            Times.Once);
        
        _commentRepositoryMock.Verify(
            repo => repo.DeleteAsync(commentId),
            Times.Once);
    }

    [Fact]
    public async Task DeleteCommentAsync_ShouldThrowNotFoundException_WhenCommentDoesNotExist()
    {
        //Arrange
        var commentId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();
        
        _commentRepositoryMock
            .Setup(repo => repo.GetByIdAsync(commentId))
            .ReturnsAsync((CommentDto)null);
        
        //Act
        var act = async () => await _commentService.DeleteCommentAsync(commentId, requestingUserId);
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        
        _commentRepositoryMock.Verify(
            repo => repo.GetByIdAsync( commentId),
            Times.Once);
        
        _commentRepositoryMock.Verify(
            repo => repo.DeleteAsync(commentId),
            Times.Never);
    }

    [Fact]
    public async Task DeleteCommentAsync_ShouldThrowForbiddenAccessException_WhenCommentIsForbidden()
    {
        //Arrange
        var commentId = Guid.NewGuid();
        var ownerUserId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();

        var existingComment = new CommentDto
        {
            Id = commentId,
            UserId = ownerUserId,
        };
        
        _commentRepositoryMock.
            Setup(repo => repo.GetByIdAsync(commentId))
            .ReturnsAsync(existingComment);
        
        //Act
        var act = async () => await _commentService.DeleteCommentAsync(commentId, requestingUserId);
        
        //Assert
        await Assert.ThrowsAsync<ForbiddenAccessException>(act);
        
        _commentRepositoryMock.Verify(
            repo => repo.GetByIdAsync(commentId),
            Times.Once);
        
        _commentRepositoryMock.Verify(
            repo => repo.DeleteAsync(commentId),
            Times.Never);
    }
        
}