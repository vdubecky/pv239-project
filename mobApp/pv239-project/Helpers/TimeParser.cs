namespace pv239_project.Helpers;

public static class TimeParser
{
    /// <summary>
    /// Parses a time string into a DateTimeOffset object and converts it to local time.
    /// </summary>
    /// <param name="time">correct time string in ISO 8601 format (e.g., "2025-06-01T11:10:06.3951754Z")</param>
    public static DateTimeOffset ParseTime(this string time)
    {
        var parsedTime = DateTimeOffset.Parse(time);
        return parsedTime.ToLocalTime();
    }
}