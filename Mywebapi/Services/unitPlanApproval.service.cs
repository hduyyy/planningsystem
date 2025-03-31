using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;
using Mywebapi.Dtos.UnitPlanApproval;

namespace Mywebapi.Services
{
    public class UnitPlanApprovalService
    {
        private readonly AppDbContext _context;

        public UnitPlanApprovalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UnitPlanApproval>> GetAllUnitPlanApprovals(int page, int limit)
        {
            return await _context.UnitPlanApprovals
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<UnitPlanApproval?> GetUnitPlanApprovalById(int id)
        {
            return await _context.UnitPlanApprovals.FindAsync(id);
        }

        public async Task<UnitPlanApproval> CreateUnitPlanApproval(CreateUnitPlanApprovalRequest request)
        {
            var approval = new UnitPlanApproval
            {
                UnitTaskId = request.UnitTaskId,
                ApprovalBy = request.ApprovalBy,
                Status = request.Status,
                ApprovalDate = request.ApprovalDate ?? DateTime.UtcNow,
                Remark = request.Remark
            };

            _context.UnitPlanApprovals.Add(approval);
            await _context.SaveChangesAsync();
            return approval;
        }

        public async Task<bool> UpdateUnitPlanApproval(int id, UpdateUnitPlanApprovalRequest request)
        {
            var approval = await _context.UnitPlanApprovals.FindAsync(id);
            if (approval == null) return false;

            approval.Status = request.Status ?? approval.Status;
            approval.ApprovalDate = request.ApprovalDate ?? approval.ApprovalDate;
            approval.Remark = request.Remark ?? approval.Remark;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUnitPlanApproval(int id)
        {
            var approval = await _context.UnitPlanApprovals.FindAsync(id);
            if (approval == null) return false;

            _context.UnitPlanApprovals.Remove(approval);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
