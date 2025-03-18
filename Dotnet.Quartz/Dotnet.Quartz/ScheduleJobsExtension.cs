using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Dotnet.Quartz;

public static class ScheduleJobsExtension
{
    private const string Scheduling = "Scheduling";
    private const string OurJob = "OurJob";

    public static void AddScheduleJob(this IServiceCollection services, IConfiguration configuration)
    {
        var scheduling = configuration[Scheduling];

        if (scheduling == null || scheduling == "false")
        {
            return;
        }

        var schedulingInterval = configuration[$"{Scheduling}:{OurJob}"];

        if (string.IsNullOrEmpty(schedulingInterval))
        {
            throw new InvalidOperationException("Schedule job not found.");
        }

        services.AddQuartz(options =>
        {
            var jobKey = JobKey.Create(nameof(OurJob));
            options
                .AddJob<OurJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger => trigger.ForJob(jobKey).WithCronSchedule(schedulingInterval));
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }
}
