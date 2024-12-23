using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models.Conversation;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ConversationService : IConversationService
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IMapper _mapper;
    
    public ConversationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ConversationsModel> GetByUserIdAsync(int userId)
    {
        var conversations = await _unitOfWork.ConversationRepository.GetManyByUserIdAsync(userId);
        return _mapper.Map<ConversationsModel>(conversations, opts => opts.Items["UserId"] = userId);
    }
    
    public async Task<ConversationModel> GetByIdAsync(int userId, int conversationId)
    {
        if (await _unitOfWork.ConversationRepository.GetByIdAsync(conversationId) == null)
        {
            throw new NotFoundException("Conversation not found");
        }
        var conversation = await _unitOfWork.ConversationRepository.GetByIdAsync(conversationId);
        return _mapper.Map<ConversationModel>(conversation, opts => opts.Items["UserId"] = userId);
    }
    
    public async Task CreateAsync(int currentUserId, ConversationCreateModel model)
    {
        if (model.ParticipantIds.All(id => id != currentUserId))
        {
            throw new BadRequestException("You can't create conversation without yourself");
        }
        if (model.GroupDetails is null && model.ParticipantIds.Count() != 2)
        {
            throw new BadRequestException("Invalid number of participants for private chat");
        }
        var conversation = _mapper.Map<Conversation>(model);
        conversation.IsGroup = model.GroupDetails is not null;
        foreach (var participantId in model.ParticipantIds)
        {
            conversation.Participants.Add(new ConversationParticipant
            {
                UserId = participantId,
                Role = participantId == currentUserId 
                    ? ConversationRole.Owner
                    : ConversationRole.Member
            });
        } ;
        await _unitOfWork.ConversationRepository.AddAsync(conversation);
        await _unitOfWork.SaveAsync();
        if (model.GroupDetails is not null)
        {
            conversation.GroupDetails.ConversationId = conversation.Id;
            _unitOfWork.GroupDetailsRepository.Update(conversation.GroupDetails);
            await _unitOfWork.SaveAsync();
        }
    }
    
    public async Task UpdateAsync(int conversationId, int currentUserId, ConversationUpdateModel model)
    {
        var conversation = await _unitOfWork.ConversationRepository.GetByIdAsync(conversationId);
        if (conversation is null)
        {
            throw new NotFoundException("Conversation not found");
        }

        if (!conversation.IsGroup)
        {
            throw new BadRequestException("You can't update private chat");
        }
        
        var participant = await _unitOfWork.ConversationParticipantRepository.GetByIdAsync(conversationId, currentUserId);
        if (participant is null || participant.Role == ConversationRole.Member)
        {
            throw new ForbiddenException("You don't have permission to update this conversation");
        }

        var ownerId = conversation.Participants.FirstOrDefault(p => p.Role == ConversationRole.Owner).UserId;
        
        if (model.ParticipantIdsToRemove.Any(id => ownerId == id))
        {
            throw new BadRequestException("You can't remove owner from conversation");
        }

        foreach (var participantId in model.ParticipantIdsToAdd)
        {
            if (!conversation.Participants.Any(p => p.UserId == participantId))
            {
                conversation.Participants.Add(new ConversationParticipant
                {
                    ConversationId = conversationId,
                    UserId = participantId,
                    Role = ConversationRole.Member
                });
            }
        }
        
        foreach (var participantId in model.ParticipantIdsToRemove)
        {
            var participantToRemove = conversation.Participants.FirstOrDefault(p => p.UserId == participantId);
            if (participantToRemove is not null)
            {
                conversation.Participants.Remove(participantToRemove);
            }
        }
        
        conversation.GroupDetails.Name = model.Name ?? conversation.GroupDetails.Name;
        conversation.GroupDetails.Description = model.Description ?? conversation.GroupDetails.Description;
        conversation.GroupDetails.AvatarLink = model.AvatarLink ?? conversation.GroupDetails.AvatarLink;
        _unitOfWork.ConversationRepository.Update(conversation);
        await _unitOfWork.SaveAsync();
    }
    
    public async Task UpdateParticipantAsync(int conversationId, int currentUserId, int userId, ConversationParticipantUpdateModel model)
    {
        var conversation = await _unitOfWork.ConversationRepository.GetByIdAsync(conversationId);
        if (conversation is null)
        {
            throw new NotFoundException("Conversation not found");
        }
        
        var participant = conversation.Participants.FirstOrDefault(p => p.UserId == currentUserId);
        if (participant is null || participant.Role == ConversationRole.Member)
        {
            throw new ForbiddenException("You don't have permission to update role");
        }
        
        var participantToUpdate = conversation.Participants.FirstOrDefault(p => p.UserId == userId);
        if (participantToUpdate is null)
        {
            throw new NotFoundException("Participant not found");
        }
        
        _mapper.Map(model, participantToUpdate);
        
        _unitOfWork.ConversationRepository.Update(conversation);
        await _unitOfWork.SaveAsync();
    }
}