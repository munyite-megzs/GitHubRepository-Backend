//using GitRepositoryTracker.Models;

using Octokit;

namespace GitRepositoryTracker.Interfaces
{
    public interface IGitHubAPIService
    {
        Task<IEnumerable<Repository>> GetAllRepositoriesBySize(int size = 2000, int page = 2, int perPage = 10);
        //Task<IEnumerable<RepositoryDto>> GetAllRepositoriesByLanguage(string language, int page, int perPage);
        //Task<IEnumerable<RepositoryDto>> GetAllRepositoriesByTopic(string topic, int page, int perPage);
    }
}
