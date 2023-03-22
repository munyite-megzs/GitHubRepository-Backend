using GitRepositoryTracker.DButil;
using GitRepositoryTracker.Interfaces;
using GitRepositoryTracker.Models;

namespace GitRepositoryTracker.Repositories
{
    public class GitAPIRepository : IGitAPIRepository
    {
        private readonly GitRepoContext _context;
        
        public GitAPIRepository(GitRepoContext gitRepoContext)
        {
            _context = gitRepoContext;
        }

        public async Task AddRepository(Repository repository)
        {
            await _context.AddAsync(repository);
        }

        public async Task AddTopic(Topic topic)
        {
            await _context.AddAsync(topic);
        }

        public async Task Addtopics(IEnumerable<Topic> topics)
        {
            await _context.AddAsync(topics);
        }

        public async Task AddRepositories(IEnumerable<Repository> repositories)
        {
            await _context.AddRangeAsync(repositories);
        }
    }
}
