using Business.Models.Message;

namespace Business.Interfaces;

public interface IMessageService
{
    Task<MessageModel> GetByIdAsync(int messageId);

    Task CreateAsync(int userId, MessageCreateModel messageModel);

    Task UpdateAsync(int userId, int messageId, MessageUpdateModel messageModel);

    Task DeleteAsync(int userId, int messageId);
}