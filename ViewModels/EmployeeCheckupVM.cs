using System.ComponentModel.DataAnnotations;
using MedicalCheckUpASP.Models;

namespace MedicalCheckUpASP.ViewModels
{
    public class EmployeeCheckupVM
    {
        public Employee Employee { get; set; }

        [Required]
        public List<CheckupHistory> CheckupHistories { get; set; }
    }
}
