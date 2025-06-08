namespace VibeMoment.Extensions;

public static class DateTimeExtensions
{
    public static bool CanEdit(this DateTime addedAt, int editLimitHours) => 
        DateTime.UtcNow.Subtract(addedAt).TotalHours < editLimitHours;
}