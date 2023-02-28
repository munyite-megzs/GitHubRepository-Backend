using GitRepositoryTracker.Interfaces;
using GitRepositoryTracker.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Octokit;
using Octokit.Internal;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace GitRepositoryTracker.Services
{
    public class GithubAPIClient : IGitHubAPIClient,IGitHubAuthService
    {

        private readonly IGitHubClient _gitHubClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public GithubAPIClient(IConfiguration configuration, IGitHubClient gitHubClient)
        {
            _configuration = configuration;
            _gitHubClient = gitHubClient;
            var productInfo = new ProductHeaderValue("GitRepoTracker");
            var accessToken = GetAccessToken();
            var credentials = new Credentials(accessToken);
            _gitHubClient = new GitHubClient(productInfo)
            {
                Credentials = credentials
            };   


        }
        public string GetAccessToken()
        {
            var accessToken = _configuration.GetValue<string>("GitHubAccessToken");
            return accessToken;
        }

        public async Task<IEnumerable<Models.Repository>> GetAllRepositoriesBySize(int size, int page, int perPage)
        {

            var repositories = new List<Models.Repository>();

            // Keep fetching repositories until we have enough

            var request = new SearchRepositoriesRequest
            {
                Size = new Octokit.Range(size),
                Page = page,
                PerPage = perPage,
                SortField = RepoSearchSort.Updated,
                Order = SortDirection.Descending
            };

            SearchRepositoryResult result;

            do
            {
                result = await _gitHubClient.Search.SearchRepo(request);
                repositories.AddRange(result.Items.Select(r => new Models.Repository
                {
                    RepositoryId = r.NodeId,
                    RepositoryName = r.Name,
                    Description = r.Description,
                    Url = r.HtmlUrl,
                    language = r.Language,
                    StargazersCount = r.StargazersCount,
                    ForksCount = r.ForksCount,
                    CreatedAt = r.CreatedAt.UtcDateTime,
                    UpdatedAt = r.UpdatedAt.UtcDateTime,
                    PushedAt = r.PushedAt?.UtcDateTime ?? DateTime.MinValue
                }));

                if (result.TotalCount > repositories.Count)
                {
                    request.Page++;
                }
            } 
            while (result.TotalCount > repositories.Count && result.Items.Count == perPage);

            return repositories;
        }


        private static IEnumerable<(Models.Repository, IEnumerable<Topic>, IEnumerable<RepositoryTopic>)> DeserializeRepositories(IEnumerable<Octokit.Repository> repositories)
        {
            var result = new List<(Models.Repository, IEnumerable<Topic>, IEnumerable<RepositoryTopic>)>();

            foreach (var repo in repositories)
            {
                var repository = new Models.Repository
                {
                    RepositoryId = repo.NodeId,
                    RepositoryName = repo.Name,
                    Description = repo.Description,
                    Url = repo.HtmlUrl,
                    language = repo.Language,
                    StargazersCount = repo.StargazersCount,
                    ForksCount = repo.ForksCount,
                    CreatedAt = repo.CreatedAt.UtcDateTime,
                    UpdatedAt = repo.UpdatedAt.UtcDateTime,
                    PushedAt = repo.PushedAt?.UtcDateTime ?? DateTime.MinValue
                };

                var topics = repo.Topics.Select(t => new Topic { TopicName = t }).ToList();

                var repositoryTopics = topics.Select(t => new RepositoryTopic { RepositoryId = repo.NodeId, Topic = t }).ToList();

                result.Add((repository, topics, repositoryTopics));
            }

            return result;
        }

        Task<IEnumerable<Octokit.Repository>> IGitHubAPIClient.GetAllRepositoriesBySize(int size, int page, int perPage)
        {
            throw new NotImplementedException();
        }
    }
}











