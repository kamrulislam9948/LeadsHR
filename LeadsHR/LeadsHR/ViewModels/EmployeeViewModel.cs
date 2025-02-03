using LeadsHR.Models;

namespace LeadsHR.ViewModels
{
    public class EmployeeViewModel
    {
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
        public IEnumerable<EducationInfo> EducationInfos { get; set; } = new List<EducationInfo>();
    }
    public class EmployeeEditViewModel
    {
        public Employee Employee { get; set; }
        public List<EducationInfo> EducationInfos { get; set; }
    }
}
