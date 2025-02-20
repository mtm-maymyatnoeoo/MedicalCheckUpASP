using System.Collections.Generic;
using MedicalCheckUpASP.Models;
using MedicalCheckUpASP.Services.EmployeeService;
using MedicalCheckUpASP.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MedicalCheckUpASP.Controllers
{
    [Authorize] // require login

    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            //InitializeData();

            return View();
        }

        [Route("employee/get-packages")]
        [HttpGet]
        public JsonResult GetPackages()
        {
            List<SelectListItem> packageList = _employeeService.GetPackageList();
            return Json(packageList);
        }
        //private void InitializeData()
        //{
        //    ViewBag.Packages = _employeeService.GetPackageList();
        //}
    }
}
