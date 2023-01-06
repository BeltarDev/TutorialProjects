using Microsoft.EntityFrameworkCore;
using TimeRegistration.BusinessLogic.TimeRegistration;

namespace TimeRegistration.UI.TimeRegistration
{
    public static class TimeRegistrationExtensions
    {
        public static WebApplicationBuilder ConfigureTimeRegistrationFeature(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TimeRegistrationDbContext>(x =>
            {
                x.UseInMemoryDatabase("TimeReg");
            });

            builder.Services.AddScoped<TimeRegistrationService>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddHostedService<TimeRegistrationSeedService>();
            }

            return builder;
        }
    }
}
