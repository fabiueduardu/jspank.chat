using JSpank.Chat.App.Interfaces.Services;
using JSpank.Chat.App.Services;
using JSpank.Chat.Domain.Interfaces.Repositories;
using JSpank.Chat.Domain.Interfaces.Services;
using JSpank.Chat.Domain.Services;
using JSpank.Chat.Infra.Data.Repositories;
using SimpleInjector;

namespace JSpank.Chat.CrossCutting.DI
{
    public class DiContainer
    {
        public static void RegisterAll(Container container)
        {
            //domain repositories
            container.Register<IAppRepository, AppRepository>(Lifestyle.Transient);
            container.Register<IPostRepository, PostRepository>(Lifestyle.Transient);

            //domain services
            container.Register<IAppService, AppService>(Lifestyle.Transient);
            container.Register<IPostService, PostService>(Lifestyle.Transient);


            //app services
            container.Register<IAppAppService, AppAppService>(Lifestyle.Transient);
            container.Register<IPostAppService, PostAppService>(Lifestyle.Transient);

        }
    }
}
