using LibraryManagement.Web.Data;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDBContext dBContext;

        public DashboardController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

       
        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Retrieve Most Available Genre using SQL query
            var mostAvailableGenreQuery = @"
                SELECT TOP 1 Genre, COUNT(*) AS Count
                FROM Books
                where Availability = 'True'
                GROUP BY Genre
                ORDER BY Count DESC
            ";

            var mostAvailableGenre = await dBContext.Books
                .FromSqlRaw(mostAvailableGenreQuery)
                .Select(b => new { b.Genre })
                .FirstOrDefaultAsync();

            // Retrieve Most Sold Genre using SQL query
            var mostSoldGenreQuery = @"
                SELECT TOP 1 Genre, COUNT(*) AS Count
                FROM Books
                where Availability ='False'
                GROUP BY Genre
                ORDER BY Count DESC
            ";

            var mostSoldGenre = await dBContext.Books
                .FromSqlRaw(mostSoldGenreQuery)
                .Select(b => new { b.Genre })
                .FirstOrDefaultAsync();

            // Retrieve Number of Active Members
            var numberOfActiveMembers = await dBContext.Members
                .CountAsync(m => m.Status);

            // Prepare view model to pass data to view
            var viewModel = new DashboardViewModel
            {
                MostAvailableGenre = mostAvailableGenre?.Genre,
                MostSoldGenre = mostSoldGenre?.Genre,
                NumberOfActiveMembers = numberOfActiveMembers
            };

            return View(viewModel);
        }
    }
}
