using LibraryManagement.Web.Data;
using LibraryManagement.Web.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext dBContext;

        public AccountController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        //Login View
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



    }
}
