using LibraryManagement.Web.Data;
using LibraryManagement.Web.Models.Entities;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibraryManagement.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDBContext dbContext;

        public BookController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //View Add Book
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //Save a book
        [HttpPost]
        public async Task<IActionResult>Add(AddBookvViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = viewModel.Title,
                    Author = viewModel.Author,
                    Genre = viewModel.Genre,
                    Availability = viewModel.Availability,
                };
                await dbContext.Books.AddAsync(book);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(viewModel);
        }

        //Show All Books
        [HttpGet]
        public async Task<IActionResult>List()
        {
            var books = await dbContext.Books.ToListAsync();

            return View(books);
        }

        //show Details of Book
        [HttpGet]
        public async Task<IActionResult>Edit(Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            return View(book);
        }

        //Edit Book
        [HttpPost]
        public async Task<IActionResult>Edit(Book viewModel)
        {
            var book = await dbContext.Books.FindAsync(viewModel.BookID);
            if (book != null) 
            {
                book.Title=viewModel.Title;
                book.Author = viewModel.Author;
                book.Genre = viewModel.Genre;
                book.Availability = viewModel.Availability;
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Book");
        }


    }
}
