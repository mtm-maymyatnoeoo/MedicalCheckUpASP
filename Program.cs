using System.Configuration;
using MedicalCheckUpASP.DbContexts;
using MedicalCheckUpASP.Services.LoginService;
using MedicalCheckUpASP.Services.UserService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure EF Core with MySQL
// Register CommonContext and UserContext with DI container
//builder.Services.AddDbContext<CommonDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
//    ),
//    ServiceLifetime.Scoped

//);
builder.Services.AddDbContext<CommonDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))
    ), ServiceLifetime.Scoped); // Scoped means one instance per request
//builder.Services.AddDbContext<UserContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
//    ),
//    ServiceLifetime.Scoped
//);
//// Register SqlRawHelper
//builder.Services.AddScoped<SqlRawHelper>();

// Add services to the container.
//builder.Services.AddControllersWithViews();
// Register the service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add authentication and configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/admin/login"; // Set login path
        options.LogoutPath = "/admin/logout"; // Set logout path
        //options.AccessDeniedPath = "/admin/AccessDenied"; // Set access denied path
    });

// Add authorization
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Use session middleware to enable session for the application
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add this line for authentication
app.UseAuthentication();

// Add this line for authorization
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

var routes = new (string name, string pattern, object defaults)[]
{
    ("default", "{controller=Home}/{action=Index}/{id?}", new { }),
    ("users", "users/{action=Index}/{id?}", new { controller = "Users" }),
    ("search", "users/{action}/{searchQuery?}", new {controller = "Users", action = "Search"}),
    ("newUser", "users/new", new {controller = "Users", action = "New"}),
    ("createUser", "users/create", new {controller = "Users", action = "Create"}),
    ("loginAdmin", "admin/login", new {controller = "Login", action = "AdminLogin"}),
    ("logoutAdmin", "admin/logout", new {controller = "Login", action = "Logout"})
};

// Register routes dynamically
foreach (var (name, pattern, defaults) in routes)
{
    app.MapControllerRoute(name, pattern, defaults);
}


app.Run();
