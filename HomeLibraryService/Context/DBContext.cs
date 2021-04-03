using HomeLibraryData.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeLibraryService.Context
{
    public class DBContext: DbContext
    {

        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DBContext() {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=HomeLibraryDatabase;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasMany(i => i.Genres).WithMany(i => i.Books);
            modelBuilder.Entity<Genre>().HasMany(i => i.Books).WithMany(i => i.Genres);

            base.OnModelCreating(modelBuilder);
        }
    }
}
