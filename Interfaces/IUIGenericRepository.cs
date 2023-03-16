using GitRepositoryTracker.DTO;

namespace GitRepositoryTracker.Interfaces
{
    public interface IUIGenericRepository
    {

        Task<IEnumerable<RepositoryDto>> GetAllByTopic(string topicName);
        Task<IEnumerable<RepositoryDto>> GetAllByLanguage(string language);
        Task<IEnumerable<RepositoryDto>> GetAllByStars();
        Task<IEnumerable<RepositoryDto>> GetAllByForks();
        Task<IEnumerable<RepositoryDto>> GetAllByDate();
        Task<IEnumerable<RepositoryDto>> GetAllRepositories();
        Task<IEnumerable<TopicDto>> GetAllTopicsAsync();

    }
}
