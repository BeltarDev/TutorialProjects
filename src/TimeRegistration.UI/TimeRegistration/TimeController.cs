using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TimeRegistration.UI.TimeRegistration;

[ApiController]
[Route("api/[controller]")]
public class TimeController : ControllerBase
{
    private readonly TimeRegistrationDbContext _dbContext;

    public TimeController(TimeRegistrationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateModel model)
    {
        var dbModel = new TimeEntry
        {
            Title = model.Title,
            Description = model.Description,
            StartTime = model.StartTime
        };

        await _dbContext.AddAsync(dbModel);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction("GetById", TimeEntryDto.Create(dbModel));
    }


    [HttpGet]
    public async Task<IActionResult> Get(int pageSize, int pageNumber, string? orderBy)
    {
        var query = _dbContext.TimeEntries.AsQueryable();
        if (orderBy?.Equals(nameof(TimeEntry.Title), StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderBy(x => x.Title);
        }
        else if (orderBy?.Equals($"{nameof(TimeEntry.Title)}_desc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderByDescending(x => x.Title);
        }
        else if (orderBy?.Equals(nameof(TimeEntry.Description), StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderBy(x => x.Description);
        }
        else
        {
            query = query.OrderBy(x => x.Id);
        }

        query = query.Skip(pageSize * (pageNumber - 1));

        query = query.Take(pageSize);

        var results = await query.Select(x => TimeEntryDto.Create(x)).ToListAsync();

        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entry = await _dbContext.TimeEntries.FindAsync(id);
        return entry == null ? NotFound() : Ok(TimeEntryDto.Create(entry));
    }
}
