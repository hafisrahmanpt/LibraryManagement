using LibraryManagement.Web.Data;
using LibraryManagement.Web.Models;
using LibraryManagement.Web.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Numerics;

namespace LibraryManagement.Web.Controllers
{
    public class MemberController : Controller
    {

        private readonly ApplicationDBContext dbContext;

        public MemberController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //View Add Member Page
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //Save a member
        [HttpPost]
        public async Task<IActionResult> Add(AddMemberViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var member = new Member
                {
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    Phone = viewModel.Phone,
                    Status = viewModel.Status,
                    Email = viewModel.Email,
                    Password = viewModel.Password
                };

                await dbContext.Members.AddAsync(member);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(viewModel);
        }

        //Show All members
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var members = await dbContext.Members.ToListAsync();

            return View(members);
        }

        [HttpGet]
        public async Task<IActionResult>Edit(Guid id)
        {
            var member = await dbContext.Members.FindAsync(id);
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult>Edit(Member viewModel)
        {
            var member = await dbContext.Members.FindAsync(viewModel.MemberID); 
            if (member != null)
            {
               member.Name=viewModel.Name;
               member.Address = viewModel.Address;
               member.Phone = viewModel.Phone;
               member.Status = viewModel.Status;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Member");
        }
    }
}
