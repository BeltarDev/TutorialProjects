using ConsoleDI.Example.Interfaces;

namespace ConsoleDI.Example;

internal sealed class ExampleSingletonService : IExampleSingletonService
{
    Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid(); 
}
