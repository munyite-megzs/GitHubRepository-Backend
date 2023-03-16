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



            CreateMap<DateTimeOffset, DateTime>().ConvertUsing(dto => dto.UtcDateTime);

            CreateMap<Octokit.Repository, RepositoryDto>()
                .ForMember(dest => dest.RepositoryId, opt => opt.MapFrom(src => src.NodeId))
                .ForMember(dest => dest.RepositoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.UtcDateTime));


            //CreateMap<RepositoryTopicDto, RepositoryTopic>()
            //    .ForMember(dest => dest.Repository, opt => opt.Ignore())
            //    .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => new Topic { TopicName = src.Topicdto.TopicName }));

            //CreateMap<RepositoryTopic, RepositoryTopicDto>()
            //    .ForMember(dest => dest.RepositoryId, opt => opt.MapFrom(src => src.RepositoryId))
            //    .ForMember(dest => dest.Topicdto, opt => opt.MapFrom(src => new TopicDto { TopicName = src.Topic.TopicName }));          


        }
        //public MappingConfigs()
        //{
        //    CreateMap<Repository, RepositoryDto>().ReverseMap();
        //    CreateMap<Topic, TopicDto>().ReverseMap();
        //    CreateMap<RepositoryTopic, RepositoryTopicDto>().ReverseMap();

        //    CreateMap<DateTimeOffset, DateTime>().ConvertUsing(dto => dto.UtcDateTime);

        //    CreateMap<Octokit.Repository, RepositoryDto>()
        //        .ForMember(dest => dest.RepositoryId, opt => opt.MapFrom(src => src.NodeId))
        //        .ForMember(dest => dest.RepositoryName, opt => opt.MapFrom(src => src.Name))
        //        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime))
        //        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.UtcDateTime));

        //    CreateMap<RepositoryTopicDto, RepositoryTopic>()
        //        .ForMember(dest => dest.Repository, opt => opt.Ignore())
        //        .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => new Topic { TopicName = src.Topicdto.TopicName }));

        //    CreateMap<RepositoryTopic, RepositoryTopicDto>()
        //        .ForMember(dest => dest.RepositoryId, opt => opt.MapFrom(src => src.RepositoryId))
        //        .ForMember(dest => dest.Topicdto, opt => opt.MapFrom(src => new TopicDto { TopicName = src.Topic.TopicName }));
        //}




    }
}
