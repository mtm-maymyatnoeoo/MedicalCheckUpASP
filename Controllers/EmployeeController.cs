using System.Collections.Generic;
using MedicalCheckUpASP.Models;
using MedicalCheckUpASP.Services.EmployeeService;
using MedicalCheckUpASP.Services.UserService;
using MedicalCheckUpASP.ViewModels;
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
            var viewModel = new EmployeeCheckupVM
            {
                Employee = new Employee { EmployeeNumber = "", Name = "", Position = "", NRC = "", DOB = "", EntryDate = ""},
                CheckupHistories = new List<CheckupHistory>()
            };

            return View(viewModel);
        }

        [Route("employee/get-packages")]
        [HttpGet]
        public JsonResult GetPackages()
        {
            List<SelectListItem> packageList = _employeeService.GetPackageList();
            return Json(packageList);
        }

        [Route("employee/create")]
        public async Task<IActionResult> Create(EmployeeCheckupVM employee_checkup)
        {
            var viewModel = new EmployeeCheckupVM
            {
                Employee = employee_checkup.Employee,
                CheckupHistories = null
            };
            //return View(employee_checkup);
            if (ModelState.IsValid)
            {
                //await _employeeService.CreateUserAsync(user);
                return RedirectToAction("Index");  // Redirect to the Index page after creating the user

            }
            return View("New", employee_checkup);  // Stay on the same page and show errors
        }
        //private void InitializeData()
        //{
        //    ViewBag.Packages = _employeeService.GetPackageList();
        //}
    }
}
