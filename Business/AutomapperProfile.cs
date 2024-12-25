using AutoMapper;
using Business.Models.Auth;
using Business.Models.Conversation;
using Business.Models.Message;
using Business.Models.User;
using Data.Entities;

namespace Business;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<SignUpModel, User>()
            .ForMember(u => u.Id, opt => opt.Ignore())
            .ForMember(u => u.CreatedAt, opt => opt.Ignore())
            .ForMember(u => u.Password, opt => opt.Ignore());

        CreateMap<string, TokenModel>()
            .ForMember(tm => tm.Token, opt => opt.MapFrom(s => s));

        CreateMap<User, UserModel>()
            .ForMember(um => um.JoinedAt, opt => opt.MapFrom(u => u.CreatedAt));

        CreateMap<User, ShortUserModel>();

        CreateMap<IEnumerable<User>, UsersModel>()
            .ForMember(um => um.Users, opt => opt.MapFrom(u => u));

        CreateMap<Friendship, UserFriendshipModel>()
            .ForMember(ufm => ufm.ReceiverName, opt => opt.MapFrom(f => f.Receiver.Name))
            .ForMember(ufm => ufm.ReceiverSurname, opt => opt.MapFrom(f => f.Receiver.Surname))
            .ForMember(ufm => ufm.ReceiverAvatarLink, opt => opt.MapFrom(f => f.Receiver.AvatarLink))
            .ForMember(ufm => ufm.SenderName, opt => opt.MapFrom(f => f.Sender.Name))
            .ForMember(ufm => ufm.SenderSurname, opt => opt.MapFrom(f => f.Sender.Surname))
            .ForMember(ufm => ufm.SenderAvatarLink, opt => opt.MapFrom(f => f.Sender.AvatarLink));

        CreateMap<IEnumerable<Friendship>, UserFriendshipsModel>()
            .ForMember(ufm => ufm.Friendships, opt => opt.MapFrom(f => f));

        CreateMap<Message, MessageModel>()
            .ForMember(mm => mm.Name, opt => opt.MapFrom(m => m.Sender.Name))
            .ForMember(mm => mm.Surname, opt => opt.MapFrom(m => m.Sender.Surname));

        CreateMap<IEnumerable<Conversation>, ConversationsModel>()
            .ForMember(cm => cm.Conversations, opt => opt.MapFrom((src, dest, _, context) =>
            {
                var userId = (int)context.Items["UserId"];
                return src.Select(c => new ShortConversationModel
                {
                    Id = c.Id,
                    Name = c.IsGroup
                        ? c.GroupDetails.Name
                        : c.Participants.First(p => p.UserId != userId).User.Name,
                    AvatarLink = c.IsGroup
                        ? c.GroupDetails.AvatarLink
                        : c.Participants.First(p => p.UserId != userId).User.AvatarLink,
                    IsGroup = c.IsGroup
                });
            }));

        CreateMap<Conversation, ConversationModel>()
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom((src, dest, _, context) =>
                {
                    var userId = (int)context.Items["UserId"];
                    return src.IsGroup
                        ? src.GroupDetails.Name
                        : Enumerable.FirstOrDefault(src.Participants, p => p.UserId != userId).User.Name;
                }))
            .ForMember(dest => dest.Description, opt =>
                opt.MapFrom(src => src.IsGroup ? src.GroupDetails.Description : null))
            .ForMember(dest => dest.AvatarLink, opt =>
                opt.MapFrom((src, dest, _, context) =>
                {
                    var userId = (int)context.Items["UserId"];
                    return src.IsGroup
                        ? src.GroupDetails.AvatarLink
                        : Enumerable.FirstOrDefault(src.Participants, p => p.UserId != userId).User.AvatarLink;
                }))
            .ForMember(dest => dest.Messages, opt =>
                opt.MapFrom(src => src.Messages))
            .ForMember(dest => dest.Participants, opt =>
                opt.MapFrom(src => src.Participants));

        CreateMap<Message, ConversationMessageModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Sender.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Sender.Surname));

        CreateMap<Message, MessageModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Sender.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Sender.Surname));

        CreateMap<ConversationParticipant, ConversationUserModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.User.Surname))
            .ForMember(dest => dest.AvatarLink, opt => opt.MapFrom(src => src.User.AvatarLink))
            .ForMember(dest => dest.JoinedAt, opt => opt.MapFrom(src => src.User.CreatedAt));

        CreateMap<UserUpdateModel, User>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UserFriendshipUpdateModel, Friendship>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ConversationCreateModel, Conversation>()
            .ForMember(dest => dest.IsGroup, opt => opt.MapFrom(src => src.GroupDetails != null))
            .ForMember(dest => dest.GroupDetails, opt => opt.MapFrom(src => src.GroupDetails));

        CreateMap<ConversationCreateGroupDetailsModel, GroupDetails>()
            .ForMember(dest => dest.ConversationId, opt => opt.Ignore());

        CreateMap<ConversationParticipantUpdateModel, ConversationParticipant>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<MessageCreateModel, Message>()
            .ForMember(m => m.CreatedAt, opt => opt.Ignore());

        CreateMap<MessageUpdateModel, Message>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}