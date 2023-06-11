using AndonLights.Services.Interfaces;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace AndonLights.Services;

public class TimedHostedService : IHostedService, IDisposable
{
    private readonly ILogger<TimedHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Timer? _dailyTimer = null;
    private Timer? _monthlyTimer = null;
    private DateTime _dayOfLastDailyUpdate;
    private DateTime _monthOfLastMonthlyUpdate;

    public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _dayOfLastDailyUpdate = DateTime.MinValue;
        _monthOfLastMonthlyUpdate = DateTime.MinValue;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _dailyTimer = new Timer(DoDailyWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(30));
        _monthlyTimer = new Timer(DoMonthlyWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        _logger.LogInformation("Timed Hosted Service running.");
        return Task.CompletedTask;
    }

    private void DoMonthlyWork(object? state)
    {
        var today = DateTime.Today;
        bool isUpdateNeeded = false;
        if (today.Year == _monthOfLastMonthlyUpdate.Year && today.Month > _monthOfLastMonthlyUpdate.Month)
        {
            isUpdateNeeded = true;
        }
        if (today.Year > _monthOfLastMonthlyUpdate.Year)
        {
            isUpdateNeeded = true;
        }
        if (isUpdateNeeded)
        {
            using var scope = _serviceProvider.CreateScope();
            var stateService = scope.ServiceProvider.GetService<IStateService>();
            if (IsNotNull(stateService))
            {
                stateService.UpdateAllMonthlyStats();
                _monthOfLastMonthlyUpdate = today;
                _logger.LogInformation($"Timed Hosted Service has updated {today}-s monthly statistics.");
            }
            else
            {
                _logger.LogInformation($"StateService is null in Timed Hosted Service,DoMonthlyWork ");
            }
        }
    }

    private void DoDailyWork(object? state)
    {
        var today = DateTime.Today;
        if (DateTime.Compare(_dayOfLastDailyUpdate, today) < 0)
        {
            using var scope = _serviceProvider.CreateScope();
            var stateService = scope.ServiceProvider.GetService<IStateService>();
            if (IsNotNull(stateService))
            {
                stateService.UpdateAllDailyStats();
                _dayOfLastDailyUpdate = today;
                _logger.LogInformation($"Timed Hosted Service has updated {today}-s daily statistics.");
            }
            else
            {
                _logger.LogInformation($"StateService is null in Timed Hosted Service,DoDailyWork ");
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
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

    private bool IsNotNull([NotNullWhen(true)] object? obj)
    {
        return obj != null;
    }
}