using Quartz;

namespace Dotnet.Quartz;

public class OurJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("lets execute our job.");
        await Task.CompletedTask;
    }
}
