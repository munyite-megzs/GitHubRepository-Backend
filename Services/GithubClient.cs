using GitRepositoryTracker.Interfaces;
using GitRepositoryTracker.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Octokit;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace GitRepositoryTracker.Services
{
    public class GithubAPIClient : IGitHubAPIClient
    {

        private readonly IGitHubClient _gitHubClient;
        private readonly IConfiguration _configuration;

        public GithubAPIClient(IConfiguration configuration, IGitHubClient gitHubClient)
        {
            _configuration = configuration;
            _gitHubClient = gitHubClient;

            var productInfo = new ProductHeaderValue("GitRepoTracker");
            var accessToken = _configuration.GetValue<string>("GitHubAccessToken");
            var credentials = new Credentials(accessToken);
            _gitHubClient = new GitHubClient(productInfo)
            {
                Credentials = credentials
            };       
                

        }
        public async Task<IEnumerable<Octokit.Repository>> GetAllRepositoriesBySize(int count)
        {
            //throw new NotImplementedException(); 
            const int pageSize = 100;
            var repositories = new List<Models.Repository>();

            // Keep fetching repositories until we have enough
            for (int page = 1; repositories.Count < count; page++)
            {
                var searchRequest = new SearchRepositoriesRequest
                {
                    PerPage = pageSize,
                    Page = page,
                    Size = new Octokit.Range(200000, int.MaxValue),
                    SortField = RepoSearchSort.Stars,
                    Order = SortDirection.Descending
                };

                var searchResult = await _gitHubClient.Search.SearchRepo(searchRequest);

                repositories.AddRange(searchResult.Items.Select(r => new Models.Repository
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
            }


            //return repositories.Take(count);
        }

        public Task<IEnumerable<Octokit.Repository>> GetAllRepositoriesByLanguage(string language, int page, int perPage)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Octokit.Repository>> GetAllRepositoriesByTopic(string topic, int page, int perPage)
        {
            throw new NotImplementedException();
        }


    }
}











