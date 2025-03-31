using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;

namespace Mywebapi.Services
{
    public class UnitAttachmentService
    {
        private readonly AppDbContext _context;
        private readonly string _uploadPath;

        public UnitAttachmentService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _uploadPath = configuration["FileStorage:UploadPath"] ?? "uploads";
        }

        public async Task<List<UnitAttachment>> GetAllUnitAttachments(int page, int limit)
        {
            return await _context.UnitAttachments
                .OrderByDescending(u => u.UploadedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<UnitAttachment?> GetUnitAttachmentById(int id)
        {
            return await _context.UnitAttachments.FindAsync(id);
        }

        public async Task<UnitAttachment> UploadAndCreateUnitAttachment(IFormFile file, int unitPlanTaskId, int uploadedBy)
        {
            if (file == null || file.Length == 0)
                throw new Exception("File không hợp lệ!");

            bool unitTaskExists = await _context.UnitPlanTasks.AnyAsync(up => up.UnitTaskId == unitPlanTaskId);
            if (!unitTaskExists)
                throw new Exception($"UnitPlanTaskId {unitPlanTaskId} không tồn tại!");

            string uploadFolder = Path.Combine(_uploadPath, "uploads");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var allowedMimeTypes = new[] { "application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" };
            if (!allowedMimeTypes.Contains(file.ContentType))
                throw new Exception("Chỉ hỗ trợ tải lên file PDF và Word!");

            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(uploadFolder, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lưu file: " + ex.Message);
            }

            var attachment = new UnitAttachment
            {
                UnitPlanTaskId = unitPlanTaskId,
                FilePath = filePath,
                UploadedBy = uploadedBy,
                UploadedAt = DateTime.UtcNow
            };

            try
            {
                _context.UnitAttachments.Add(attachment);
                await _context.SaveChangesAsync();
                return attachment;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Lỗi DB: {dbEx.Message}");
                Console.WriteLine($"Inner Exception: {dbEx.InnerException?.Message}");
                Console.WriteLine($"Stack Trace: {dbEx.StackTrace}");

                // Nếu muốn, bạn có thể throw lại lỗi này cho phía client hoặc log ở nơi khác
                throw new Exception("Lỗi khi lưu attachment vào cơ sở dữ liệu.", dbEx);
            }
        }


        public async Task<UnitAttachment?> UpdateUnitAttachment(int id, IFormFile? file, int unitPlanTaskId, int uploadedBy)
        {
            var attachment = await _context.UnitAttachments.FindAsync(id);
            if (attachment == null)
                return null;

            if (file != null && file.Length > 0)
            {
                string uploadFolder = Path.Combine(_uploadPath, "uploads");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                var allowedMimeTypes = new[] { "application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" };
                if (!allowedMimeTypes.Contains(file.ContentType))
                    throw new Exception("Chỉ hỗ trợ tải lên file PDF và Word!");

                string fileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(uploadFolder, fileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi lưu file: " + ex.Message);
                }

                attachment.FilePath = filePath;
            }

            attachment.UnitPlanTaskId = unitPlanTaskId;
            attachment.UploadedBy = uploadedBy;
            attachment.UploadedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return attachment;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật dữ liệu vào database: " + ex.Message);
            }
        }


        public async Task<bool> DeleteUnitAttachment(int id)
        {
            var attachment = await _context.UnitAttachments.FindAsync(id);
            if (attachment == null)
                return false;

            if (!string.IsNullOrEmpty(attachment.FilePath) && File.Exists(attachment.FilePath))
            {
                try
                {
                    File.Delete(attachment.FilePath);
                }
                catch (IOException)
                {
                    throw new Exception("Không thể xóa file, có thể file đang được sử dụng.");
                }
            }

            try
            {
                _context.UnitAttachments.Remove(attachment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa dữ liệu: " + ex.Message);
            }
        }
    }
}
