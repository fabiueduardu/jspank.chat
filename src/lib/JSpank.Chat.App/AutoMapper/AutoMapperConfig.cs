using AutoMapper;

namespace JSpank.Chat.App.AutoMapper
{
    public class AutoMapperConfig
    {
        static MapperConfiguration mapperConfiguration = null;
        static IMapper _IMapper = null;

        public static void RegisterMappings()
        {
            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });

            _IMapper = mapperConfiguration.CreateMapper();
        }

        public static TDestination Get<TSource, TDestination>(TSource model)
        {
            return (TDestination)_IMapper.Map(model, typeof(TSource), typeof(TDestination));
        }

        public static TDestination UpdatedDestination<TSource, TDestination>(TSource modelSource, TDestination modelDestination)
        {
            return _IMapper.Map<TSource, TDestination>(modelSource, modelDestination);
        }
    }
}
