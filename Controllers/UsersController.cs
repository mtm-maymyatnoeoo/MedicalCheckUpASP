using MedicalCheckUpASP.Models;
using MedicalCheckUpASP.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalCheckUpASP.Controllers
{
    [Authorize] // require login
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService salesCategoryService)
        {
            _userService = salesCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve the LoginUserName from TempData
            //var username = TempData["LoginUserName"] as string;
            //var username = User.Identity.Name;
            // Pass it to the view (or ViewData for use in the layout)
            //ViewData["LoginUserName"] = username;
            var salesCategories = await _userService.GetAllUsersAsync();
            return View(salesCategories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var salesCategory = await _userService.GetUserByIdAsync(id);
            if (salesCategory == null)
            {
                return NotFound();
            }
            return View(salesCategory);
        }

        //[HttpGet]
        public async Task<IActionResult> Search(string searchQuery)
        {
            var searchResult = await _userService.Search(searchQuery);
            // Set ViewData to retain LoginUserName across requests
 
            return View("Index", searchResult);

        }
        public IActionResult New()
        {
            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(Role)));

            return View();
        }
        [Route("create")]
        public async Task<IActionResult> Create(User user)
        {
            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(Role)));
            if (ModelState.IsValid)
            {
                var isAlreadyExist = await _userService.CheckDuplicateUser(user.EmployeeNumber, user.Email);
                if (!isAlreadyExist)
                {
                    await _userService.CreateUserAsync(user);
                    return RedirectToAction("Index");  // Redirect to the Index page after creating the user
                }
                else
                {
                    ViewBag.Message = "Already record with this employee number or email.";
                    return View("New", user);  // Stay on the same page and show errors
                }
            }
            return View("New",user);  // Stay on the same page and show errors
        }

    }
}
