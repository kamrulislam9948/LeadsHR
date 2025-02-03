
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace LeadsHR.Models
{

    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = default!;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = default!;
        [Required, MaxLength(100)]
        public string Email { get; set; } = default!;

        [MaxLength(100)]
        public string Division { get; set; } = default!;

        [MaxLength(50)]
        public string Building { get; set; } = default!;

        [MaxLength(100)]
        public string Title { get; set; } = default!;

        [MaxLength(20)]
        public string Room { get; set; } = default!;

        // Navigation property
        public ICollection<EducationInfo> EducationInfos { get; set; } = new Collection<EducationInfo>();
    }
}
