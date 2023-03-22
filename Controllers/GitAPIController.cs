using GitRepositoryTracker.DButil;
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

        public GitAPIController (IGitAPIRepository gitAPIRepository)
        {
            _gitAPIRepository = gitAPIRepository;
        }

        [HttpPost("repository")]
        public async Task<ActionResult> AddRepository(Repository repository)
        {
            if (repository == null)
            {
                return BadRequest("No repository provided");
            }
            await _gitAPIRepository.AddRepository(repository);

            return Ok();
        }
        [HttpPost("topic")]
        public async Task<ActionResult> AddTopic(Topic topic)
        {
            if (topic == null)
            {
                return BadRequest("No topic provided");
            }
            await _gitAPIRepository.AddTopic(topic);

            return Ok();
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
