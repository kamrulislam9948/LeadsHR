using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeadsHR.Models
{
    public class EducationInfo
    {
        [Key]
        public int EducationInfoId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required, MaxLength(100)]
        public string Degree { get; set; }

        [MaxLength(100)]
        public string Institution { get; set; }

        [Required]
        public int YearOfPassing { get; set; }

        [Required]
        public string CGPA { get; set; }

        // Navigation property
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }

}
