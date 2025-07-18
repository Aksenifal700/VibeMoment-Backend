namespace VibeMoment.Infrastructure.Database.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string UserName { get; set; }
}