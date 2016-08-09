using AutoMapper;
using JSpank.Chat.App.ValueObjects;
using JSpank.Chat.Domain.Entities;
using JSpank.Chat.Domain.ValueObjects;

namespace JSpank.Chat.App.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappingProfile"; }
        }

        protected override void Configure()
        {
            this.CreateMap<ChatModel,ChatModelApp>();
            this.CreateMap<GetChatModel, GetChatModelApp>();
            this.CreateMap<Post, PostApp>();
        }
    }
}
