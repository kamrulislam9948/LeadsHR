using LeadsHR.Models;
using LeadsHR.Repository;

namespace LeadsHR.UnityOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Employee> Employees { get; }
        IRepository<EducationInfo> EducationInfos { get; }
        Task<int> SaveAsync();
    }
}
