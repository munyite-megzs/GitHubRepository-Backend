using AutoMapper;
using GitRepositoryTracker.DTO;
using GitRepositoryTracker.Interfaces;
using GitRepositoryTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitRepositoryTracker.Controllers
{
    [ApiController]
    [Route("api/GitRepoTrackerAPI")]
    public class GitAPIController: ControllerBase
    {
        private readonly IGitAPIRepository _gitAPIRepository;
        private readonly IMapper _mapper;

        public GitAPIController (IGitAPIRepository gitAPIRepository, IMapper mapper)
        {
            _gitAPIRepository = gitAPIRepository;
            _mapper = mapper;
        }

        [HttpPost("repository")]
        public async Task<ActionResult<RepositoryDto>> AddRepository(RepositoryDto repositoryDto)
        {
            if (repositoryDto == null)
            {
                return BadRequest("No repository provided");
            }

            var repository = _mapper.Map<Repository>(repositoryDto);

            await _gitAPIRepository.AddRepository(repository);

            return Ok(repository.RepositoryId);
        }
        [HttpPost("topic")]
        public async Task<ActionResult<TopicDto>> AddTopic(TopicDto topicDto)
        {
         
            if (topicDto == null)
            {
                return BadRequest("No topic provided");
            }
            var topic = _mapper.Map<Topic>(topicDto);

            await _gitAPIRepository.AddTopic(topic);

            return Ok(topic.TopicId);
        }


        [HttpPost("repositories")]
        public async Task<ActionResult<IEnumerable<Repository>>> AddRepositories(IEnumerable<Repository> repositories)
        {
            if (repositories == null || !repositories.Any())
            {
                return BadRequest("No repositories provided");
            }

            await _gitAPIRepository.AddRepositories(repositories);

            return Ok();
        }

        [HttpPost("topics")]
        public async Task<ActionResult<IEnumerable<Repository>>> AddTopics(IEnumerable<Topic> topics)
        {
            if (topics == null || !topics.Any())
            {
                return BadRequest("No topics provided");
            }

            await _gitAPIRepository.Addtopics(topics);

            return Ok();
        }

    }
}
