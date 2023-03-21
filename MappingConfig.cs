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
            CreateMap<RepositoryTopic, RepositoryTopicDto>().ReverseMap();
            CreateMap<Language, LanguageDto>().ReverseMap();

            CreateMap<DateTimeOffset, DateTime>().ConvertUsing(dto => dto.UtcDateTime);

            CreateMap<Octokit.Repository, RepositoryDto>()
                .ForMember(dest => dest.RepositoryId, opt => opt.MapFrom(src => src.NodeId))
                .ForMember(dest => dest.RepositoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.UtcDateTime))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => new LanguageDto { LanguageName = src.Language ?? "None" }));
        }


    }
}
