using Microsoft.EntityFrameworkCore;

namespace TimeRegistration.UI.TimeRegistration;

public class TimeRegistrationDbContext : DbContext
{
    public TimeRegistrationDbContext(DbContextOptions<TimeRegistrationDbContext> options) : base(options)
    {
            
    }

    public DbSet<TimeEntry> TimeEntries { get; set; }
}
