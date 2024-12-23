using Data.Interfaces;
using Data.Repositories;

namespace Data.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly SocialNetworkDbContext _context;

    public UnitOfWork(SocialNetworkDbContext context)
    {
        _context = context;
        ConversationParticipantRepository = new ConversationParticipantRepository(_context);
        ConversationRepository = new ConversationRepository(_context);
        FriendshipRepository = new FriendshipRepository(_context);
        GroupDetailsRepository = new GroupDetailsRepository(_context);
        MessageRepository = new MessageRepository(_context);
        UserRepository = new UserRepository(_context);
    }

    public IConversationParticipantRepository ConversationParticipantRepository { get; }

    public IConversationRepository ConversationRepository { get; }

    public IFriendshipRepository FriendshipRepository { get; }

    public IGroupDetailsRepository GroupDetailsRepository { get; }

    public IMessageRepository MessageRepository { get; }

    public IUserRepository UserRepository { get; }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}