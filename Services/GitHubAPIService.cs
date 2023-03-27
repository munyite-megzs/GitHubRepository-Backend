using GitRepositoryTracker.Interfaces;
using Octokit;


namespace GitRepositoryTracker.Services
{
    public class GitHubAPIService : IGitHubAPIService
    {

        private readonly IGitHubClient _gitHubClient;        

        public GitHubAPIService(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }
    
        public async Task<IEnumerable<Repository>> GetAllRepositoriesBySize(int size, int page, int perPage)
        {


            var repositories = new List<Repository>();
            var searchRequest = new SearchRepositoriesRequest
            {
                Size = Octokit.Range.GreaterThanOrEquals(size),
                Page = page,
                PerPage = perPage,
                SortField = RepoSearchSort.Stars
            };

            var searchResult = await _gitHubClient.Search.SearchRepo(searchRequest);
            repositories.AddRange(searchResult.Items);

            while (searchResult?.IncompleteResults == false)
            {
                searchRequest.Page++;
                searchResult = await _gitHubClient.Search.SearchRepo(searchRequest);
                repositories.AddRange(searchResult.Items);
            }

            return repositories;
        }
        
    }
}











