using LibraryManagement.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Web.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {          
        }
        public DbSet<Member>Members { get; set; }
        public DbSet<Book>Books { get; set; }

    }
}
