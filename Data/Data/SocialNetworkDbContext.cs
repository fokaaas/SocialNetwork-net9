using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class SocialNetworkDbContext : DbContext
{
    public SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options) : base(options)
    {
    }

    public DbSet<Conversation> Conversations { get; set; }

    public DbSet<ConversationParticipant> ConversationParticipants { get; set; }

    public DbSet<Friendship> Friendships { get; set; }

    public DbSet<GroupDetails> GroupDetails { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.Entity<ConversationParticipant>()
            .Property(cp => cp.Role)
            .HasDefaultValue(ConversationRole.Member);

        modelBuilder.Entity<ConversationParticipant>()
            .Property(cp => cp.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<ConversationParticipant>()
            .HasOne(cp => cp.User)
            .WithMany(u => u.ConversationParticipants)
            .HasForeignKey(cp => cp.UserId);

        modelBuilder.Entity<ConversationParticipant>()
            .HasOne(cp => cp.Conversation)
            .WithMany(c => c.Participants)
            .HasForeignKey(cp => cp.ConversationId);

        modelBuilder.Entity<ConversationParticipant>()
            .HasKey(cp => new { cp.UserId, cp.ConversationId });

        modelBuilder.Entity<Friendship>()
            .Property(f => f.Status)
            .HasDefaultValue(FriendshipStatus.Pending);

        modelBuilder.Entity<Friendship>()
            .Property(f => f.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.Sender)
            .WithMany(s => s.Friendships)
            .HasForeignKey(f => f.SenderId);

        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.Receiver)
            .WithMany(s => s.Friendships)
            .HasForeignKey(f => f.ReceiverId);

        modelBuilder.Entity<Friendship>()
            .HasKey(f => new { f.SenderId, f.ReceiverId });

        modelBuilder.Entity<Conversation>()
            .HasOne(c => c.GroupDetails)
            .WithOne(g => g.Conversation)
            .HasForeignKey<Conversation>(c => c.groupDetailsId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Message>()
            .Property(m => m.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(s => s.Messages)
            .HasForeignKey(m => m.SenderId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Conversation)
            .WithMany(s => s.Messages)
            .HasForeignKey(m => m.ConversationId);
    }
}