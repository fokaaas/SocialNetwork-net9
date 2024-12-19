using AutoMapper;
using Business.Models.Auth;
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
        
        CreateMap<User, UsersModel>()
            .ForMember(um => um.Users, opt => opt.MapFrom(u => u));

        CreateMap<Friendship, UserFriendshipModel>()
            .ForMember(ufm => ufm.UserId, opt => opt.MapFrom(f => f.Receiver.Id))
            .ForMember(ufm => ufm.Name, opt => opt.MapFrom(f => f.Receiver.Name))
            .ForMember(ufm => ufm.Surname, opt => opt.MapFrom(f => f.Receiver.Surname))
            .ForMember(ufm => ufm.AvatarLink, opt => opt.MapFrom(f => f.Receiver.AvatarLink));
        
        CreateMap<Friendship, UserFriendshipsModel>()
            .ForMember(ufm => ufm.Friendships, opt => opt.MapFrom(f => f));
        
        CreateMap<Message, MessageModel>()
            .ForMember(mm => mm.Name, opt => opt.MapFrom(m => m.Sender.Name))
            .ForMember(mm => mm.Surname, opt => opt.MapFrom(m => m.Sender.Surname));
    }
}