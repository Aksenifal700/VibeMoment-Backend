using VibeMoment.BusinessLogic.Enums;

namespace VibeMoment.BusinessLogic.DTOs.Auth;

public class UpdateUserProfileDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? Birthdate { get; set; }
    public string? Bio { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public Gender? Gender { get; set; }
}