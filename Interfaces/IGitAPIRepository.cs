using GitRepositoryTracker.Models;

namespace GitRepositoryTracker.Interfaces
{
    public interface IGitAPIRepository
    {
        //Task AddRepository(Repository repository);
        Task AddTopic(Topic topic);
        Task Addtopics(IEnumerable<Topic> topics);
        Task AddRepositories(IEnumerable<Octokit.Repository> repositories);
        Task<Repository> GetRepositoryById(string id);
        Task<Topic> GetTopicById(int id);

    }
}
7