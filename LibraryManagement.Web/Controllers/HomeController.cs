using LibraryManagement.Web.Data;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LibraryManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Loads Sign In Page for member
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var member = _context.Members.FirstOrDefault(m => m.Email == email && m.Password == password);

            if (member != null)
            {
                // Set the member status in session
                HttpContext.Session.SetString("IsMemberActive", member.Status.ToString());

                //Set the member name in session
                HttpContext.Session.SetString("MemberName", member.Name);

                // Set the authentication cookie or claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, member.Email)
        };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("MemberPortal", "Home");
            }
            else
            {
                ViewBag.LoginError = "Invalid email or password";
                return View("Index");
            }
        }

        public IActionResult MemberPortal()
        {
            // Fetch books list from the database
            var booksList = _context.Books.ToList();

            // Read member status from session
            var isMemberActive = HttpContext.Session.GetString("IsMemberActive");

            // Convert the status to boolean and set it in ViewBag
            if (bool.TryParse(isMemberActive, out var memberStatus))
            {
                ViewBag.IsMemberActive = memberStatus;
            }
            else
            {
                ViewBag.IsMemberActive = false;
            }
            
            //Set memberName in Viewbag
            var memberName = HttpContext.Session.GetString("MemberName");
            ViewBag.MemberName = memberName;

            return View("MemberDashboard", booksList);
        }

        [HttpGet]
        public IActionResult LibrarianLogin()
        {
            return View();
        }

        //Hardcoding the Librarian credentials here
        private const string LibrarianUserName = "Librarian321";
        private const string LibrarianPassword = "123456789";
        [HttpPost]
        public IActionResult LibrarianLogin(string username, string password)
        {
            // Check credentials
            if (username == LibrarianUserName && password == LibrarianPassword)
            {
                // Set session variable
                HttpContext.Session.SetString("IsLibrarianLoggedIn", "true");

                // Create claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim("IsLibrarian", "true") 
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                // Sign in the user                
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                
                var isAuthenticated = User.Identity.IsAuthenticated;

                // Redirect to the Dashboard List action after login
                return RedirectToAction("List", "Dashboard");
            }
            else
            {
                ViewBag.LoginError = "Invalid username or password";
                return View();
            }
        }

        // Clear session data and make the navbar hidden
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home"); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
