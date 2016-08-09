using AutoMapper;
using JSpank.Chat.App.ValueObjects;
using JSpank.Chat.Domain.Entities;
using JSpank.Chat.Domain.ValueObjects;

namespace JSpank.Chat.App.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappingProfile"; }
        }

        protected override void Configure()
        {
            this.CreateMap<ChatModelApp, ChatModel>();
            this.CreateMap<GetChatModelApp, GetChatModel>();
            this.CreateMap<PostApp, Post>();
        }
    }
}
