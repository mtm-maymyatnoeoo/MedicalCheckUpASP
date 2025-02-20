using System.Collections.Generic;
using MedicalCheckUpASP.Models;
using MedicalCheckUpASP.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalCheckUpASP.Services.EmployeeService
{
    public interface IEmployeeService
    {
        //public List<PackageHostipal> GetPackages();
        public List<SelectListItem> GetPackageList();
    }
}
