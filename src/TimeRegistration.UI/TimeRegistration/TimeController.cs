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
            Title = model.Title, Description = model.Description, StartTime = model.StartTime
        };

        await _dbContext.AddAsync(dbModel);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction("GetById", TimeEntryDto.Create(dbModel));
    }


    [HttpGet]
    public async Task<IActionResult> Get(int pageSize, int pageNumber, string? orderBy)
    {
        var query = _dbContext.TimeEntries.AsQueryable();

        var totalCount = await query.LongCountAsync();

        if (orderBy?.Equals(nameof(TimeEntry.Title), StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderBy(x => x.Title);
        }
        else if (orderBy?.Equals($"{nameof(TimeEntry.Title)}_desc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderByDescending(x => x.Title);
        }
        else if (orderBy?.Equals($"{nameof(TimeEntry.StartTime)}_desc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderByDescending(x => x.StartTime);
        }
        else if (orderBy?.Equals($"{nameof(TimeEntry.StartTime)}", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderBy(x => x.StartTime);
        }
        else if (orderBy?.Equals(nameof(TimeEntry.Description), StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderBy(x => x.Description);
        }
        else if (orderBy?.Equals($"{nameof(TimeEntry.Description)}_desc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderByDescending(x => x.Description);
        }
        else if (orderBy?.Equals(nameof(TimeEntry.Id), StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderBy(x => x.Id);
        }
        else if (orderBy?.Equals($"{nameof(TimeEntry.Id)}_desc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            query = query.OrderByDescending(x => x.Id);
        }
        else
        {
            query = query.OrderBy(x => x.Id);
        }

        query = query.Skip(pageSize * (pageNumber - 1));

        query = query.Take(pageSize);

        var results = await query.Select(x => TimeEntryDto.Create(x)).ToListAsync();

        await Task.Delay(1500);

        return Ok( new
            {
                TotalCount = totalCount,
                Records = results
            }
            );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entry = await _dbContext.TimeEntries.FindAsync(id);
        return entry == null ? NotFound() : Ok(TimeEntryDto.Create(entry));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById([FromQuery] int id)
    {
        try
        {
            var entry = await _dbContext.TimeEntries.FindAsync(id);
            if (entry == null)
            {
                return NotFound($"TimeEntry with Id = {id} not found");
            }

            _dbContext.TimeEntries.Remove(entry);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
        
    }
}
