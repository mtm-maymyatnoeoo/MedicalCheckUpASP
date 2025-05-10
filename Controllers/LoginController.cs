using System.Security.Claims;
using MedicalCheckUpASP.Models;
using MedicalCheckUpASP.Services.LoginService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace MedicalCheckUpASP.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        // GET: Login page
        public IActionResult AdminLogin()
        {
            return View();
        }

        // POST: Login page (handling login submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(Models.LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            var user = _loginService.AdminLogin(loginRequest.Email, loginRequest.Password, Role.Admin);

            if (user == null)
            {
                ViewData["ErrorMessage"] = "Invalid credentials";
                return View(loginRequest);
            }

            // Here you would set up the authentication cookie
            // (e.g., using ASP.NET Core Identity or a custom solution)

            // Set a simple cookie or session for user authentication
            //HttpContext.Session.SetString("UserId", user.Id.ToString());
            //HttpContext.Session.SetString("UserName", user.UserName.ToString());
            await setAuthentication(user);
            // Redirect to the home page after successful login
            //TempData["LoginUserName"] = user.UserName;sz
            return RedirectToAction("Index", "Users");
        }
        // Logout functionality
        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Remove("UserId");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("AdminLogin");
        }

        private async Task setAuthentication(User user)
        {
            // Creating claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            // Create identity from claims
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            // Create authentication properties (optional, e.g., remember user)
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Keeps user logged in across sessions (cookie-based persistence)
                ExpiresUtc = DateTime.UtcNow.AddMinutes(3)  // Optional: set cookie expiration time
            };

            // Sign in the user (store claims in a cookie)
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        }
    }
}
