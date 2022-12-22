using Microsoft.EntityFrameworkCore;

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

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddHostedService<TimeRegistrationSeedService>();
            }

            return builder;
        }
    }
}
