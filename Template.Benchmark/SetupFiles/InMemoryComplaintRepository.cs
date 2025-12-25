using Template.Domain.Entities;
using Template.Domain.Enums;
using Template.Domain.Pagination;
using Template.Domain.Repositories;

namespace Template.Benchmark.SetupFiles
{
    public class InMemoryComplaintRepository : IComplaintRepository
    {
        private readonly List<Complaint> _store = new();
        public Task<Complaint> AddAsync(Complaint entity)
        {
            _store.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<Complaint?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Complaint>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagedEntity<GetAllComplaintsMappingDto>> GetAllComplaintsWithUserName(int pageNum, int pageSize, EnumRoleNames userRole, string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<Complaint?> GetComplaintByIdWithFilesAsync(int complaintId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Complaint>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task HardDeleteAsync(Complaint entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync(Complaint entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Complaint entity)
        {
            throw new NotImplementedException();
        }
    }
}
