using ConsoleDI.Example.Interfaces;

namespace ConsoleDI.Example;

internal sealed class ExampleScopedService : IExampleScopedService
{
    Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
}
