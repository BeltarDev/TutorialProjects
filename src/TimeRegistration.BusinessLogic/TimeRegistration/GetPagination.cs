using System.Collections.Generic;

namespace TimeRegistration.BusinessLogic.TimeRegistration;

public class GetPagination
{
    public long TotalCount {get; set; }
    public List<TimeEntry> Records { get; set; }
}
