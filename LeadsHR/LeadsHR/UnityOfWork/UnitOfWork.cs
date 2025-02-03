using LeadsHR.Models;
using LeadsHR.Repository;

namespace LeadsHR.UnityOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Employee> Employees { get; }
        public IRepository<EducationInfo> EducationInfos { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new Repository<Employee>(_context);
            EducationInfos = new Repository<EducationInfo>(_context);
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
