using AutoMapper;
using GitRepositoryTracker.DTO;
using GitRepositoryTracker.Interfaces;
using GitRepositoryTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitRepositoryTracker.Controllers
{
    [ApiController]
    [Route("api/GitRepoTrackerAPI")]
    public class GitAPIController : ControllerBase
    {
        private readonly IGitAPIRepository _gitAPIRepository;
        private readonly IMapper _mapper;
        private readonly IGitHubAPIService _gitHubAPIService;
        private readonly IRepositoryDeserializer _repositoryDeserializer;

        public GitAPIController(IGitAPIRepository gitAPIRepository, IMapper mapper, IGitHubAPIService gitHubAPIClient, IRepositoryDeserializer repositoryDeserializer)
        {
            _gitAPIRepository = gitAPIRepository;
            _mapper = mapper;
            _gitHubAPIService = gitHubAPIClient;
            _repositoryDeserializer = repositoryDeserializer;
        }

        //[HttpPost("create_repository")]
        //public async Task<ActionResult> AddRepository(RepositoryDto repositoryDto)
        //{
        //    if (repositoryDto == null)
        //    {
        //        return BadRequest("No repository provided");
        //    }

        //    var repository = _mapper.Map<Repository>(repositoryDto);

        //    await _gitAPIRepository.AddRepository(repository);

        //    return Ok(repository.RepositoryId);
        //}
        [HttpPost("create_topic")]
        public async Task<ActionResult> AddTopic(TopicDto topicDto)
        {

            if (topicDto == null)
            {
                return BadRequest("No topic provided");
            }
            var topic = _mapper.Map<Topic>(topicDto);

            await _gitAPIRepository.AddTopic(topic);

            return Ok(topic.TopicId);
        }


        //[HttpPost("add_repositories")]
        //public async Task<ActionResult> AddRepositories(IEnumerable<RepositoryDto> repositoryDtos)
        //{
        //    if (repositoryDtos == null || !repositoryDtos.Any())
        //    {
        //        return BadRequest("No repositories provided");
        //    }
        //    var repositories = _mapper.Map<IEnumerable<Repository>>(repositoryDtos);

        //    await _gitAPIRepository.AddRepositories(repositories);

        //    return Ok();
        //}

        [HttpPost("add_topics")]
        public async Task<ActionResult> AddTopics(IEnumerable<TopicDto> topicDtos)
        {
            if (topicDtos == null || !topicDtos.Any())
            {
                return BadRequest("No topics provided");
            }

            var topics = _mapper.Map<IEnumerable<Topic>>(topicDtos);

            await _gitAPIRepository.Addtopics(topics);

            return Ok();
        }

        [HttpGet("repository/{id}")]
        public async Task<ActionResult<RepositoryDto>> GetRepositoryById(string id)
        {
            var repository = await _gitAPIRepository.GetRepositoryById(id);

            if (repository == null)
            {
                return NotFound();
            }
            var repositoryDto = _mapper.Map<RepositoryDto>(repository);

            return Ok(repositoryDto);
        }

        [HttpGet("gettopic/{id}")]
        public async Task<ActionResult<RepositoryDto>> GetTopicById(int id)
        {
            var topic = await _gitAPIRepository.GetTopicById(id);

            if (topic == null)
            {
                return NotFound();
            }
            var topicDto = _mapper.Map<TopicDto>(topic);

            return Ok(topicDto);
        }

        [HttpPost("repositories")]
        public async Task<IActionResult> AddRepositories(int size, int page, int perPage)
        {

            try
            {
                var repositories = await _gitHubAPIService.GetAllRepositoriesBySize(size, page, perPage);
                await _gitAPIRepository.AddRepositories(repositories);

                return Ok("Repositories added to database successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding repositories to database: {ex.Message}");
            }


        }
    }
}
