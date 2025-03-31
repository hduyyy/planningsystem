
using Microsoft.EntityFrameworkCore;
using Mywebapi.Dtos.PersonalPlanApproval;
using Mywebapi.Models;

namespace Mywebapi.Services
{
    public class PersonalPlanApprovalService
    {
        private readonly AppDbContext _context;

        public PersonalPlanApprovalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PersonalPlanApproval>> GetAllPersonalPlanApprovals(int page, int limit)
        {
            return await _context.PersonalPlanApprovals
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<PersonalPlanApproval?> GetPersonalPlanApprovalById(int id)
        {
            return await _context.PersonalPlanApprovals.FindAsync(id);
        }

        public async Task<PersonalPlanApproval> CreatePersonalPlanApproval(CreatePersonalPlanApprovalDto request)
        {
            var approval = new PersonalPlanApproval
            {
                TaskId = request.TaskId,
                ApprovalBy = request.ApprovalBy,
                ApprovalDate = request.ApprovalDate,
                Status = request.Status,
                Remark = request.Remark
            };

            _context.PersonalPlanApprovals.Add(approval);
            await _context.SaveChangesAsync();
            return approval;
        }

        public async Task<bool> UpdatePersonalPlanApproval(int id, UpdatePersonalPlanApprovalDto request)
        {
            var approval = await _context.PersonalPlanApprovals.FindAsync(id);
            if (approval == null)
                return false;

            approval.TaskId = request.TaskId ?? approval.TaskId;
            approval.ApprovalBy = request.ApprovalBy ?? approval.ApprovalBy;
            approval.ApprovalDate = request.ApprovalDate ?? approval.ApprovalDate;
            approval.Status = request.Status ?? approval.Status;
            approval.Remark = request.Remark ?? approval.Remark;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePersonalPlanApproval(int id)
        {
            var approval = await _context.PersonalPlanApprovals.FindAsync(id);
            if (approval == null)
                return false;

            _context.PersonalPlanApprovals.Remove(approval);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}