using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeRegistration.BusinessLogic;
using TimeRegistration.BusinessLogic.TimeRegistration;
using Xunit;
using Xunit.Abstractions;

namespace TimeRegistration.Tests
{
    public class TimeRegistrationServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TimeRegistrationServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private static ServiceProvider CreateServiceProvider(string dbName)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<TimeRegistrationService>();
            serviceCollection.AddDbContext<TimeRegistrationDbContext>(builder =>
            {
                builder.UseInMemoryDatabase(dbName);
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }

        [Fact]
        public async Task Shall_Create_Test_Successfully()
        {
            // arrange
            var serviceProvider = CreateServiceProvider(nameof(Shall_Create_Test_Successfully));
            var dbContext = serviceProvider.GetRequiredService<TimeRegistrationDbContext>();
            var service = serviceProvider.GetRequiredService<TimeRegistrationService>();

            var model = new CreateModel
            {
                Title = "title",
                Description = "desc",
                StartTime = DateTime.UtcNow
            };

            // act
            var result = await service.Create(model);

            // assert
            result.Should().NotBeNull();
            result!.Title.Should().Be(model.Title);
            result.Description.Should().Be(model.Description);
            result.StartTime.Should().Be(model.StartTime);

            var dbEntry = await dbContext.TimeEntries.FindAsync(result.Id);
            dbEntry.Should().NotBeNull();
            dbEntry!.Title.Should().Be(model.Title);
            dbEntry.Description.Should().Be(model.Description);
            dbEntry.StartTime.Should().Be(model.StartTime);
        }

        [Fact]
        public async Task Shall_GetById_Test_Successfully()
        {
            // arrange
            var serviceProvider = CreateServiceProvider(nameof(Shall_GetById_Test_Successfully));
            var dbContext = serviceProvider.GetRequiredService<TimeRegistrationDbContext>();
            var service = serviceProvider.GetRequiredService<TimeRegistrationService>();

            var model = new CreateModel { Title = "title", Description = "desc", StartTime = DateTime.UtcNow };
            var result = await service.Create(model);

            // act

            var findEntry = await service.GetById(result.Id);

            // assert
            findEntry.Should().NotBeNull();
            findEntry!.Title.Should().Be(model.Title);
            findEntry.Description.Should().Be(model.Description);
            findEntry.StartTime.Should().Be(model.StartTime);
            findEntry.Id.Should().Be(1, becauseArgs: result.Id);
        }

        [Fact]
        public async Task Shall_DeleteById_Test_Succesfully()
        {
            // arrange
            var serviceProvider = CreateServiceProvider(nameof(Shall_DeleteById_Test_Succesfully));
            var dbContext = serviceProvider.GetRequiredService<TimeRegistrationDbContext>();
            var service = serviceProvider.GetRequiredService<TimeRegistrationService>();

            var model = new CreateModel { Title = "title", Description = "desc", StartTime = DateTime.UtcNow };
            var result = await service.Create(model);
            // act
            await service.Delete(result.Id);
            //assert
            dbContext.TimeEntries.Should().BeEmpty();
        }

        [Theory]
        [InlineData(10, 5, "id_desc", 90)]
        [InlineData(20, 3, "id", 66)]
        public async Task Shall_Get_Test_Succesfully(int pageSize, int pageNumber, string orderBy, int totalCount)
        {
            //arrange
            var serviceProvider = CreateServiceProvider(nameof(Shall_Get_Test_Succesfully));
            var dbContext = serviceProvider.GetRequiredService<TimeRegistrationDbContext>();
            var service = serviceProvider.GetRequiredService<TimeRegistrationService>();

            var entries = Enumerable.Range(1, totalCount)
                .Select(x => new TimeEntry
                {
                    Id = x,
                    Title = $"Job {x}",
                    Description = $"This is #{x}",
                    StartTime = DateTime.UtcNow.AddMinutes(x ^ x)
                }).ToArray();
            await dbContext.TimeEntries.AddRangeAsync(entries);
            await dbContext.SaveChangesAsync();

            try
            {
                //act
                var result = await service.Get(pageSize, pageNumber, orderBy);

                //assert
                if (orderBy.EndsWith("_desc", StringComparison.OrdinalIgnoreCase))
                {
                    result.Records.Select(x => x.Id).Should().BeInDescendingOrder();
                }
                else
                {
                    result.Records.Select(x => x.Id).Should().BeInAscendingOrder();
                }

                foreach (var record in result.Records)
                {
                    record.Id.Should().BeInRange(pageSize * (pageNumber - 1) + 1, pageSize * pageNumber);
                    _testOutputHelper.WriteLine($"Record id: {record.Id}");
                }
                result.Records.Count.Should().Be(pageSize);
                result.TotalCount.Should().Be(totalCount);
            }
            finally
            {
                dbContext.TimeEntries.RemoveRange(entries);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
