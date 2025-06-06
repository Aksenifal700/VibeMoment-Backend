namespace VibeMoment.Extension;

public static class TimeExtension
{
    private const int EDIT_LIMIT_HOURS = 1;
    
    public static bool CanEdit(this DateTime addedAt) => 
        DateTime.UtcNow.Subtract(addedAt).TotalHours < EDIT_LIMIT_HOURS;
    
    public static string TimeAgo(this DateTime addedAt)
    {
        var diff = DateTime.UtcNow - addedAt;
        
        return diff.TotalDays >= 1 ? $"{(int)diff.TotalDays}d ago" :
            diff.TotalHours >= 1 ? $"{(int)diff.TotalHours}h ago" :
            diff.TotalMinutes >= 1 ? $"{(int)diff.TotalMinutes}m ago" : "now";
    }
}