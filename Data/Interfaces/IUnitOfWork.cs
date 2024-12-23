namespace Data.Interfaces;

public interface IUnitOfWork
{
    IConversationRepository ConversationRepository { get; }

    IConversationParticipantRepository ConversationParticipantRepository { get; }

    IMessageRepository MessageRepository { get; }

    IUserRepository UserRepository { get; }

    IFriendshipRepository FriendshipRepository { get; }

    IGroupDetailsRepository GroupDetailsRepository { get; }

    Task SaveAsync();
}