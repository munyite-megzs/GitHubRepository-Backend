using GitRepositoryTracker.Models;

namespace GitRepositoryTracker.Interfaces
{
    public interface IUIGenericRepository
    {
        
        Task <IEnumerable<Repository>> GetAllByTopic(string topicName);
        Task <IEnumerable<Repository>> GetAllByLanguage(string language);
        Task<IEnumerable<Repository>> GetAllByStars();
        Task<IEnumerable<Repository>>GetAllByForks();
        Task<IEnumerable<Repository>>GetAllByDate();
        Task<IEnumerable<Repository>> GetAllRepositories();
        Task<IEnumerable<Topic>> GetAllTopicsAsync();

    }
}
