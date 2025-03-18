public class Program
{
    public async Task StartAsync()
    {
        Console.WriteLine("Starting job. YEAH");
        await Task.CompletedTask;
    }

    public async Task StoptAsync()
    {
        Console.WriteLine("Stoping job. ow");
        await Task.CompletedTask;
    }
}