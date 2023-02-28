using AutoMapper;
using GitRepositoryTracker.DTO;
using GitRepositoryTracker.Models;

namespace GitRepositoryTracker
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        { 
            CreateMap<Repository, RepositoryDto>().ReverseMap();
            CreateMap<Topic, TopicDto>().ReverseMap();
        }
    }
}
