using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models.Message;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class MessageService : IMessageService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateAsync(int userId, MessageCreateModel model)
    {
        var message = _mapper.Map<Message>(model);
        message.SenderId = userId;
        await _unitOfWork.MessageRepository.AddAsync(message);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int userId, int messageId)
    {
        var message = await _unitOfWork.MessageRepository.GetByIdAsync(messageId);
        if (message is null) throw new NotFoundException("Message not found");
        if (message.SenderId != userId)
            throw new ForbiddenException("You don't have permission to delete this message");
        await _unitOfWork.MessageRepository.DeleteByIdAsync(messageId);
        await _unitOfWork.SaveAsync();
    }

    public async Task<MessageModel> GetByIdAsync(int messageId)
    {
        var message = await _unitOfWork.MessageRepository.GetByIdAsync(messageId);
        if (message is null) throw new NotFoundException("Message not found");
        return _mapper.Map<MessageModel>(message);
    }

    public async Task UpdateAsync(int userId, int messageId, MessageUpdateModel model)
    {
        var message = await _unitOfWork.MessageRepository.GetByIdAsync(messageId);
        if (message is null) throw new NotFoundException("Message not found");
        if (message.SenderId != userId)
            throw new ForbiddenException("You don't have permission to update this message");
        _mapper.Map(model, message);
        await _unitOfWork.SaveAsync();
    }
}