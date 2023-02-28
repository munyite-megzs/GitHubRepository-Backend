//using GitRepositoryTracker.Models;

using Octokit;

namespace GitRepositoryTracker.Interfaces
{
    public interface IGitHubAPIClient
    {
        Task <IEnumerable<Repository>> GetAllRepositoriesBySize (int size, int page, int perPage);
        //Task<IEnumerable<RepositoryDto>> GetAllRepositoriesByLanguage(string language, int page, int perPage);
        //Task<IEnumerable<RepositoryDto>> GetAllRepositoriesByTopic(string topic, int page, int perPage);
    }
}
