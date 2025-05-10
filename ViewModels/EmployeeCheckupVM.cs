using System.ComponentModel.DataAnnotations;
using MedicalCheckUpASP.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace MedicalCheckUpASP.ViewModels
{
    public class EmployeeCheckupVM
    {
        public Employee Employee { get; set; }

        [Required(ErrorMessage = "Checkup histories is required.")]
        public List<CheckupHistory> CheckupHistories { get; set; }
    }
}
