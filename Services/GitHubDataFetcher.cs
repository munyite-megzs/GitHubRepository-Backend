using GitRepositoryTracker.Interfaces;
using Microsoft.Extensions.Logging;

namespace GitRepositoryTracker.Services
{
    public class GitHubDataFetcher : IHostedService,IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<GitHubDataFetcher> _logger;
        private Timer _timer;
        private readonly int _size;
        private readonly int _page;
        private readonly int _perPage;
        private readonly TimeSpan _fetchInterval;

        public GitHubDataFetcher(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration, ILogger<GitHubDataFetcher> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _size = configuration.GetValue<int>("GitHubDataFetcherSettings:Size");
            _page = configuration.GetValue<int>("GitHubDataFetcherSettings:Page");
            _perPage = configuration.GetValue<int>("GitHubDataFetcherSettings:PerPage");
            _fetchInterval = TimeSpan.FromMinutes(configuration.GetValue<double>("GitHubDataFetcherSettings:FetchIntervalInMinutes"));
           
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(FetchData, null, TimeSpan.Zero, _fetchInterval);
            return Task.CompletedTask;
        }
        private async void FetchData(object state)
        {
            _logger.LogInformation("Fetching data started.");

            using var scope = _serviceScopeFactory.CreateScope();
            var gitHubAPIService = scope.ServiceProvider.GetRequiredService<IGitHubAPIService>();
            var gitAPIRepository = scope.ServiceProvider.GetRequiredService<IGitAPIRepository>();

            var repositories = await gitHubAPIService.GetAllRepositoriesBySize(_size, _page, _perPage);
            await gitAPIRepository.AddRepositories(repositories);

            _logger.LogInformation("Fetching data completed. {Count} repositories added.", repositories.Count());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
