using TimeRegistration.BusinessLogic.TimeRegistration;

namespace TimeRegistration.UI.API;

public class TimeEntryDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public static TimeEntryDto Create(TimeEntry timeEntry)
    {
        return new TimeEntryDto
        {
            Id = timeEntry.Id,
            Title = timeEntry.Title,
            Description = timeEntry.Description,
            StartTime = timeEntry.StartTime
        };
    }
}
