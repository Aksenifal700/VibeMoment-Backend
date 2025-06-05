namespace VibeMoment.Database.Entities;


public class Photo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public byte[] Data { get; set; } = [];
    
    public DateTime AddedAt { get; set; }
    
    public TimeSpan TimeSinceAdded => DateTime.UtcNow - AddedAt;
    
    public int DaysOld => (int)TimeSinceAdded.TotalDays;
    public int HoursOld => (int)TimeSinceAdded.TotalHours;
    public int MinutesOld => (int)TimeSinceAdded.TotalMinutes;
    public string TimeAgo => GetTimeAgoString();
    
    private string GetTimeAgoString()
    {
        var timeSpan = TimeSinceAdded;
        
        if (timeSpan.TotalDays >= 1)
            return $"{(int)timeSpan.TotalDays} days ago";
        if (timeSpan.TotalHours >= 1)
            return $"{(int)timeSpan.TotalHours} hours ago";
        if (timeSpan.TotalMinutes >= 1)
            return $"{(int)timeSpan.TotalMinutes} minutes ago";
        
        return "Just now";
    }
}   