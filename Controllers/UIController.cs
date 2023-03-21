using AutoMapper;
using GitRepositoryTracker.DTO;
using GitRepositoryTracker.Interfaces;
using GitRepositoryTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitRepositoryTracker.Controllers
{
    [ApiController]
    [Route("api/GitRepoTrackerAPI")]
    public class UIController : ControllerBase
    {
        private readonly IUIGenericRepository _uIGenericRepository;
        private readonly IMapper _mapper;

        public UIController(IUIGenericRepository uIGenericRepository, IMapper mapper)
        {
            _uIGenericRepository = uIGenericRepository;
            _mapper = mapper;
        }


        [HttpGet("all-repositories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllRepostories()
        {

            var repositories = await _uIGenericRepository.GetAllRepositories();
            if (repositories == null || !repositories.Any())
            {
                return NotFound();
            }

            return Ok(repositories);
        }

        [HttpGet("by-updated-at")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllByUpdatedAt()
        {

            var repositories = await _uIGenericRepository.GetAllByDate();
            if (repositories == null || !repositories.Any())
            {
                return NotFound();
            }

            return Ok(repositories);
        }


        [HttpGet("topic/{topicName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RepositoryDto>>> GetAllByTopic(string topicName)
        {
            if (string.IsNullOrEmpty(topicName))
            {
                return BadRequest("TopicDto parameter is required");
            }

            var repositories = await _uIGenericRepository.GetAllByTopic(topicName);

            if (repositories == null || !repositories.Any())
            {
                return NotFound();
            }
            return Ok(repositories);
        }

        [HttpGet("by-forks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Repository>>> GetAllByNumberOfForks()
        {

            var repositories = await _uIGenericRepository.GetAllByForks();
            if (repositories == null || !repositories.Any())
            {
                return NotFound();
            }


            return Ok(repositories);

        }

        [HttpGet("by-stars")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Repository>>> GetAllByNumberOfStars()
        {

            var repositories = await _uIGenericRepository.GetAllByStars();
            if (repositories == null || !repositories.Any())
            {
                return NotFound();
            }

            return Ok(repositories);

        }

        [HttpGet("language/{languageName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Repository>>> GetAllByLanguage(string languageName)
        {
            if (string.IsNullOrEmpty(languageName))
            {
                return BadRequest("Language parameter is required");
            }
            var repositories = await _uIGenericRepository.GetAllByLanguage(languageName);
            if (repositories == null || !repositories.Any())
            {
                return NotFound();
            }

            return Ok(repositories);

        }

        [HttpGet("all-topics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTopics()
        {

            var topics = await _uIGenericRepository.GetAllTopicsAsync();
            if (topics == null || !topics.Any())
            {
                return NotFound();
            }

            return Ok(topics);
        }

        [HttpGet("all-languages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllLanguages()
        {

            var languages = await _uIGenericRepository.GetAllLanguagesAsync();
            if (languages == null || !languages.Any())
            {
                return NotFound();
            }

            return Ok(languages);
        }
    }
}
