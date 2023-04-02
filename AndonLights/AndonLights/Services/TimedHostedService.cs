using AndonLights.Services.Interfaces;
using System.ComponentModel;

public class TimedHostedService : IHostedService, IDisposable
{
    private readonly ILogger<TimedHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Timer? _dailyTimer = null;
    private Timer? _monthlyTimer = null;

    private Timer? _testTimer = null;

    public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _testTimer = new Timer(DoWork,null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        _dailyTimer = new Timer(DoDailyWork, null, TimeSpan.Zero,TimeSpan.FromDays(1));
        _monthlyTimer = new Timer(DoMonthlyWork, null, TimeSpan.Zero, TimeSpan.FromDays(30));
        _logger.LogInformation("Timed Hosted Service running.");
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        Console.WriteLine(DateTime.Now);
    }

    private void DoMonthlyWork(object? state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var stateService = scope.ServiceProvider.GetService<IStateService>();
            stateService.UpdateAllMonthlyStats();
            _logger.LogInformation($"Timed Hosted Service has updated {DateTime.Today}-s monthly statistics.");
        } 
    }

    private void DoDailyWork(object? state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var stateService = scope.ServiceProvider.GetService<IStateService>();
            stateService.UpdateAllDailyStats();
            _logger.LogInformation($"Timed Hosted Service has updated {DateTime.Today}-s daily statistics.");
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _dailyTimer?.Change(Timeout.Infinite, 0);
        _monthlyTimer?.Change(Timeout.Infinite, 0);
        _logger.LogInformation("Timed Hosted Service is stopping.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _dailyTimer?.Dispose();
        _monthlyTimer?.Dispose();
    }
}