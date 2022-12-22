namespace TimeRegistration.UI.TimeRegistration;

public class TimeRegistrationSeedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public TimeRegistrationSeedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<TimeRegistrationDbContext>();

        var entries = Enumerable.Range(1, 500)
            .Select(x => new TimeEntry
            {
                Id = x,
                Title = $"Title {x}",
                Description = $"Description {x}",
                StartTime = DateTime.UtcNow
            }).ToArray();

        await dbContext.TimeEntries.AddRangeAsync(entries);

        await dbContext.SaveChangesAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
