using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Template.Application.Abstraction.Commands;
using Template.Application.Complaints.Dtos;
using Template.Application.Events;
using Template.Application.Users;
using Template.Domain;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;
using Template.Domain.Services;

namespace Template.Application.Complaints.Commands.Update;

public class UpdateComplaintCommandHandler(ILogger<UpdateComplaintCommandHandler> logger, IMapper mapper,
    IAccountRepository accountRepository, IComplaintRepository complaintRepository,
    IUserContext userContext, IFileService fileService, IDomainEventDispatcher domainEventDispatcher, IUnitOfWork unitOfWork) : ICommandHandler<UpdateComplaintCommand, ComplaintDto>
{
    public async Task<Result<ComplaintDto>> Handle(UpdateComplaintCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating complaint");
        try
        {
            await unitOfWork.BeginTransactionAsync();
            string currentUserId = userContext.GetCurrentUser()!.Id;
            var dbUser = await accountRepository.FindUserByIdOptionalTracking(currentUserId, false);

            var existingComplaint = await complaintRepository.GetComplaintByIdWithFilesAsync(request.ComplaintId);
            if (existingComplaint == null)
            {
                return Result<ComplaintDto>.Failure(new List<string> { "Complaint not found " });
            }

            if (existingComplaint.IsLocked && dbUser!.Id != existingComplaint.LockedBy)
            {
                return Result<ComplaintDto>.Failure(new List<string> { "Someone else is processing this complaint" });
            }
            complaintRepository.ApplyConcurrencyCheck(existingComplaint, request.RowVersion);
            var historyEntries = new List<History>();

            /*Processing changing fields (hopefully)*/
            if (existingComplaint.Description != request.Description && !string.IsNullOrEmpty(request.Description))
            {
                AddHistory(existingComplaint.Id, dbUser!.Id, historyEntries, ChangeType.UpdateDescription, existingComplaint.Description, request.Description);
                existingComplaint.Description = request.Description;
            }

            if (existingComplaint.Location != request.Location && !string.IsNullOrEmpty(request.Location))
            {
                AddHistory(existingComplaint.Id, dbUser!.Id, historyEntries, ChangeType.UpdateLocation, existingComplaint.Location, request.Location);
                existingComplaint.Location = request.Location;
            }

            if (request.GovernmentalEntityId.HasValue && existingComplaint.GovernmentalEntityId != request.GovernmentalEntityId)
            {
                AddHistory(existingComplaint.Id, dbUser!.Id, historyEntries, ChangeType.GovermentalEntityChange, existingComplaint.GovernmentalEntityId, request.GovernmentalEntityId);
                existingComplaint.GovernmentalEntityId = (int)request.GovernmentalEntityId;
            }
            if (request.NewStatus.HasValue && (existingComplaint.Status != request.NewStatus))
            {
                AddHistory(existingComplaint.Id, dbUser!.Id, historyEntries, ChangeType.UpdateStatus, existingComplaint.Status, request.NewStatus);
                existingComplaint.Status = request.NewStatus.Value;
            }

            /*Unlocking the complaint (hopefully)*/
            if (request.NewStatus == ComplaintStatus.Done)
            {
                existingComplaint.IsLocked = false;
                existingComplaint.LockedBy = "";
            }

            existingComplaint.Histories.AddRange(historyEntries);
            await complaintRepository.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            existingComplaint.Update();
            await domainEventDispatcher.DispatchAsync(existingComplaint.DomainEvents);
            existingComplaint.ClearDomainEvents();
            var result = mapper.Map<ComplaintDto>(existingComplaint);
            return Result<ComplaintDto>.Success(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    private static void AddHistory(int complaintId, string userId, List<History> historyEntries, ChangeType type, object oldValue, object newValue, object? details = null)
    {
        historyEntries.Add(new History
        {
            ComplaintId = complaintId,
            UserId = userId,
            ChangeType = type,
            OldValue = JsonSerializer.Serialize(oldValue),
            NewValue = JsonSerializer.Serialize(newValue),
            ChangeDetails = details != null ? JsonSerializer.Serialize(details) : string.Empty,
            CreatedAt = DateTime.UtcNow
        });
    }
}
