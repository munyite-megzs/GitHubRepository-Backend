﻿using GitRepositoryTracker.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GitRepositoryTracker.Services
{
    public class GitHubDataFetcher : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<GitHubDataFetcher> _logger;
        private Timer _timer;
        private readonly int _size;
        private readonly int _page;
        private readonly int _perPage;
        private readonly TimeSpan _fetchInterval;

        // Constructor for the GitHubDataFetcher service
        public GitHubDataFetcher(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration, ILogger<GitHubDataFetcher> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

            // Load configuration values
            _size = configuration.GetValue<int>("GitHubDataFetcherSettings:Size");
            _page = configuration.GetValue<int>("GitHubDataFetcherSettings:Page");
            _perPage = configuration.GetValue<int>("GitHubDataFetcherSettings:PerPage");
            _fetchInterval = TimeSpan.FromMinutes(configuration.GetValue<double>("GitHubDataFetcherSettings:FetchIntervalInHours"));
        }

        // Start the periodic data fetcher
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(FetchData, null, TimeSpan.Zero, _fetchInterval);
            return Task.CompletedTask;
        }

        // Fetch data from the GitHub API and store it in the repository
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

        // Stop the periodic data fetcher
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        // Dispose of the timer resource
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
