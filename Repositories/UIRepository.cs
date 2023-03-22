using GitRepositoryTracker.DButil;
using GitRepositoryTracker.Interfaces;
using GitRepositoryTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GitRepositoryTracker.Repositories
{
    public class UIRepository : IUIGenericRepository
    {
        private readonly GitRepoContext _context;

        public UIRepository(GitRepoContext gitRepoContext)
        {
            _context = gitRepoContext;
        }
        public async Task<IEnumerable<Repository>> GetAllByDate()
        {
            var result = await _context.Repositories               
                .ToListAsync();
            return result.OrderByDescending(rep=> rep.UpdatedAt);
        }

        public async Task<IEnumerable<Repository>> GetAllByForks()
        {
            var result = await _context.Repositories                
                .ToListAsync();
            return result.OrderByDescending(rep => rep.ForksCount);
        }

        public async Task<IEnumerable<Repository>> GetAllByLanguage(string language)
        {
            var result = await _context.Repositories
                .Where(rep => rep.language == language)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Repository>> GetAllByStars()
        {
            var result = await _context.Repositories          
                .ToListAsync();
            return result.OrderByDescending(rep => rep.StargazersCount);
        }

        public async Task<IEnumerable<Repository>> GetAllByTopic(string topicName)
        {
            var repositories = await _context.Repositories
                .Include(r => r.RepositoryTopics)
                .ThenInclude(rt => rt.Topic)
                .Where(r => r.RepositoryTopics.Any(rt => rt.Topic.TopicName == topicName))
                .ToListAsync();

            return repositories;
        }

        public async Task<IEnumerable<Repository>> GetAllRepositories()
        {
            var repositories = await _context.Repositories.ToListAsync();
            return repositories;
        }

        public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            return await _context.Topics.ToListAsync();
        }
    }
}
