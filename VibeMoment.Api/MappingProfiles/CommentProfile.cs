using AutoMapper;
using VibeMoment.Api.Models.Responses;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Api.MappingProfiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<AddCommentDto, Comment>();
        CreateMap<Comment, CommentDto>();

        CreateMap<CommentDto, CommentResponse>();
    }
    
}