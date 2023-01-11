using System.Dynamic;

namespace ConsoleDI.Example.Interfaces;

public interface IReportServiceLifetime
{
    Guid Id { get; }
    ServiceLifetime Lifetime { get; }
}


