using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimeRegistration.BusinessLogic.TimeRegistration
{
    public class TimeRegistrationService
    {
        private readonly TimeRegistrationDbContext _dbContext;

        public TimeRegistrationService(TimeRegistrationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TimeEntry> Create(CreateModel model)
        {
            var newTimeEntry = new TimeEntry
            {
                StartTime = model.StartTime, Description = model.Description, Title = model.Title
            };

            await _dbContext.AddAsync(newTimeEntry);
            await _dbContext.SaveChangesAsync();
            return newTimeEntry;
        }

        public async Task<TimeEntry> Create1(string title, string description, DateTime startTime)
        {
            var timeEntry = new TimeEntry { StartTime = startTime, Description = description, Title = title };

            await _dbContext.AddAsync(timeEntry);
            await _dbContext.SaveChangesAsync();
            return timeEntry;
        }

        public async Task<TimeEntry> GetById(int id)
        {
            var entry = await _dbContext.TimeEntries.FindAsync(id);
            return entry;
        }

        public async Task Delete(int id)
        {
            var entry = await _dbContext.TimeEntries.FindAsync(id);
            if (entry == null)
            {
                throw new InvalidOperationException($"TimeEntry with id:{id} not found");
            }

            _dbContext.TimeEntries.Remove(entry);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<GetPagination> Get(int pageSize, int pageNumber, string? orderBy)
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

            var results = await query.ToListAsync();

            return new GetPagination() { Records = results, TotalCount = totalCount };

        }
    }
}
