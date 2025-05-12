namespace VibeMoment.Database.Entities;


public class Photo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] Data { get; set; } = [];
}   