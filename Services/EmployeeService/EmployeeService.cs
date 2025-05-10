using System.Collections.Generic;
using MedicalCheckUpASP.DbContexts;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalCheckUpASP.Services.EmployeeService
{
    public class EmployeeService: IEmployeeService
    {
        private readonly CommonDbContext _dbContext;

        public EmployeeService(CommonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SelectListItem> GetPackageList()
        {
            //var packages = _dbContext.Packages.Select(p => new
            //{
            //    Id = p.Id,
            //    DisplayText = p.Name + " | " + p.PackageYear + " | " + p.packageType.ToString() + " | " + p.PackagePrice
            //});
            
            var packagesWithHospital = _dbContext.Packages.Join(_dbContext.HospitalInfos, p => p.HospitalId, h => h.Id, (p, h) => new
            {
                Id = p.Id,
                DisplayText = h.Name + " | " + p.Name + " | " + p.PackageYear + " | " + p.packageType.ToString() + " | " + p.PackagePrice
            }).ToList();



            return packagesWithHospital.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(), // Ensure `Value` is a string
                Text = p.DisplayText      // Assign `DisplayText` to `Text`
            }).ToList();
        }

        //public List<PackageHostipal> GetPackages()
        //{
        //    var packagesWithHospitals = _dbContext.Packages
        //        .Join(_dbContext.HospitalInfos,
        //                package => package.HospitalId,
        //                hospital => hospital.Id,
        //                (package, hospital) => new PackageHostipal
        //                {
        //                    Id = package.Id,
        //                    Name = package.Name,
        //                    PackageYear = package.PackageYear,
        //                    PackagePrice = package.PackagePrice,
        //                    OldOrNew = package.OldOrNew,
        //                    HospitalName = hospital.Name,
        //                    ContactInfo = hospital.ContactInfo,
        //                    UniqName = hospital.UniqName,

        //                })
        //        .ToList();
        //    return packagesWithHospitals;
        //}

    }
}
