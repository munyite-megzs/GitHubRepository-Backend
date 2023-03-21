using AutoMapper;
using AutoMapper.QueryableExtensions;
using GitRepositoryTracker.DButil;
using GitRepositoryTracker.DTO;
using GitRepositoryTracker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GitRepositoryTracker.Repositories
{
    public class UIRepository : IUIGenericRepository
    {
        private readonly GitRepoContext _context;
        private readonly IMapper _mapper;
        public UIRepository(GitRepoContext gitRepoContext, IMapper mapper)
        {
            _context = gitRepoContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RepositoryDto>> GetAllByDate()
        {
            var result = await _context.Repositories
                .ProjectTo<RepositoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return result.OrderByDescending(rep => rep.UpdatedAt);
        }

        public async Task<IEnumerable<RepositoryDto>> GetAllByForks()
        {
            var result = await _context.Repositories
                .ProjectTo<RepositoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result.OrderByDescending(rep => rep.ForksCount);
        }

        public async Task<IEnumerable<RepositoryDto>> GetAllByLanguage(string language)
        {
            var result = await _context.Repositories
                .Include(l=>l.Language)
                .Where(rep => rep.Language.LanguageName == language)
                .ProjectTo<RepositoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RepositoryDto>> GetAllByStars()
        {
            var result = await _context.Repositories
                .ProjectTo<RepositoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result.OrderByDescending(rep => rep.StargazersCount);
        }

        public async Task<IEnumerable<RepositoryDto>> GetAllByTopic(string topicName)
        {
            var repositories = await _context.Repositories
                .Include(r => r.RepositoryTopics)
                .ThenInclude(rt => rt.Topic)
                .Where(r => r.RepositoryTopics.Any(rt => rt.Topic.TopicName == topicName))
                .ProjectTo<RepositoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return repositories;
        }

        public async Task<IEnumerable<LanguageDto>> GetAllLanguagesAsync()
        {
            return await _context.Languages
                .ProjectTo<LanguageDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<RepositoryDto>> GetAllRepositories()
        {
            var repositories = await _context.Repositories
                .ProjectTo<RepositoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return repositories;
        }

        public async Task<IEnumerable<TopicDto>> GetAllTopicsAsync()
        {
            return await _context.Topics
                .ProjectTo<TopicDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
